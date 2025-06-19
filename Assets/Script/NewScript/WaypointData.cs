using UnityEngine;
using UnityEngine.Assertions;

namespace ChangeLab.VRMusicAcademy
{
    public class WaypointData : MonoBehaviour
    {
       [field: SerializeField] public AudioClip audioClip {get; private set;}

        void OnValidate()
        {
            Assert.IsNotNull(audioClip, $"{audioClip} cannot be null in {name}");
        }
    }
}