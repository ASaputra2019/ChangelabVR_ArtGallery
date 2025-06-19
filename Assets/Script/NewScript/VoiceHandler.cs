using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace ChangeLab.VRMusicAcademy.Player
{

    public partial class NPCController : MonoBehaviour
    {

        private const string MOUTH_OPEN_BLEND_SHAPE_NAME = "mouthOpen";
        private const int AMPLITUDE_MULTIPLIER = 10;
        private const int AUDIO_SAMPLE_LENGTH = 4096;
        private const string MISSING_BLENDSHAPE_MESSAGE = "The 'mouthOpen' morph target is required for VoiceHandler.cs but it was not found on Avatar mesh. Use an AvatarConfig to specify the blendshapes to be included on loaded avatars.";

        private float[] audioSample = new float[AUDIO_SAMPLE_LENGTH];
        private static readonly string[] HeadMeshNameFilter = { "Renderer_Head", "Renderer_Avatar", "Renderer_Head_Custom" };

        private const string BEARD_MESH_NAME_FILTER = "Renderer_Beard";
        private const string TEETH_MESH_NAME_FILTER = "Renderer_Teeth";

        // ReSharper disable InconsistentNaming
        private Dictionary<SkinnedMeshRenderer, int> blendshapeMeshIndexMap;

        private readonly MeshType[] faceMeshTypes = { MeshType.HeadMesh, MeshType.BeardMesh, MeshType.TeethMesh };
        private bool CanGetAmplitude => audioSource != null && audioSource.clip != null && audioSource.isPlaying;


        private void Start()
        {
            CreateBlendshapeMeshMap();
            if (!HasMouthOpenBlendshape())
            {
                Debug.LogWarning(MISSING_BLENDSHAPE_MESSAGE);
               // enabled = false;
                return;
            }
        }

        private bool HasMouthOpenBlendshape()
        {
            foreach (KeyValuePair<SkinnedMeshRenderer, int> blendshapeMeshIndex in blendshapeMeshIndexMap)
            {
                if (blendshapeMeshIndex.Value >= 0)
                {
                    return true;
                }

            }
            return false;
        }
        public SkinnedMeshRenderer GetMeshRenderer(MeshType meshType)
        {
            SkinnedMeshRenderer mesh;
            var children = new List<SkinnedMeshRenderer>(
    gameObject.GetComponentsInChildren<SkinnedMeshRenderer>()
); 

            if (children.Count == 0)
            {

                return null;
            }

            switch (meshType)
            {
                case MeshType.BeardMesh:
                    mesh = children.FirstOrDefault(child => BEARD_MESH_NAME_FILTER == child.name);
                    break;
                case MeshType.TeethMesh:
                    mesh = children.FirstOrDefault(child => TEETH_MESH_NAME_FILTER == child.name);
                    break;
                case MeshType.HeadMesh:
                    mesh = children.FirstOrDefault(child => HeadMeshNameFilter.Contains(child.name));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(meshType), meshType, null);
            }

            if (mesh != null) return mesh;
            return null;
        }

        private void CreateBlendshapeMeshMap()
        {
            blendshapeMeshIndexMap = new Dictionary<SkinnedMeshRenderer, int>();
            foreach (MeshType faceMeshType in faceMeshTypes)
            {
                SkinnedMeshRenderer faceMesh = GetMeshRenderer(faceMeshType);
                if (faceMesh)
                {
                    TryAddSkinMesh(faceMesh);
                }
            }
        }

        private void TryAddSkinMesh(SkinnedMeshRenderer skinMesh)
        {
            if (skinMesh != null)
            {
                var index = skinMesh.sharedMesh.GetBlendShapeIndex(MOUTH_OPEN_BLEND_SHAPE_NAME);
                blendshapeMeshIndexMap.Add(skinMesh, index);
            }
        }

        private void AudioUpdate()
        {
            // var value = GetAmplitude();
            // print(value);
            SetBlendShapeWeights((Mathf.Sin(Time.time * 9) + 1f) * 0.25f);
        }
        
        private float GetAmplitude()
        {
            if (CanGetAmplitude && audioSource.clip.loadState == AudioDataLoadState.Loaded)
            {
                var currentPosition = audioSource.timeSamples;
                var remaining = audioSource.clip.samples - currentPosition;
                if (remaining >= 0 && remaining < AUDIO_SAMPLE_LENGTH)
                {
                    return 0f;
                }

                audioSource.clip.GetData(audioSample, audioSource.timeSamples);
                var amplitude = 0f;

                foreach (var sample in audioSample)
                {
                    amplitude += Mathf.Abs(sample);
                }

                return Mathf.Clamp01(amplitude / audioSample.Length * AMPLITUDE_MULTIPLIER);
            }

            return 0f;
        }

        private void SetBlendShapeWeights(float weight)
        {
            foreach (KeyValuePair<SkinnedMeshRenderer, int> blendshapeMeshIndex in blendshapeMeshIndexMap)
            {
                if (blendshapeMeshIndex.Value >= 0)
                {
                    blendshapeMeshIndex.Key.SetBlendShapeWeight(blendshapeMeshIndex.Value, weight);
                }
            }
        }

    }
    public enum MeshType
    {
        BeardMesh = 0,
        TeethMesh = 1,
        HeadMesh = 2,
    }
}

