using System;
using System.Collections;
using UnityEngine;
using ChangeLab.ArtGallery.Player;

namespace ChangeLab.ArtGallery
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private NPCController[] npcController;
        [SerializeField] private Transform playerController;
        private NPCManager nPCManager;
        void Start()
        {
            nPCManager = new NPCManager(npcController, playerController);
            if (playerController.TryGetComponent<FPSController>(out var fPSController))
            {
                fPSController.nPCManager = nPCManager;
            }
        }
    }

}
