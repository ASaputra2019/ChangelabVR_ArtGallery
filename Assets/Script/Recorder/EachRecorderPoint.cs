using System;
using ChangeLab.ArtGallery.Player;
using UnityEngine;

namespace ChangeLab.ArtGallery.Recorder
{
    public class EachRecorderPoint : MonoBehaviour
    {
        [SerializeField] private NPCController nPCController;
        public event Action startMovement;
        public void StartAudio()
        {
            nPCController.StartMovement(nPCController.transform.position);
            nPCController.CanInteract += StartMovement;
        }

        public void StartMovement()
        {
            startMovement?.Invoke();
            nPCController.CanInteract -= StartMovement;
        }    
    }
}

