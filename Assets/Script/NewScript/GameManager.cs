using System;
using System.Collections;
using ChangeLab.VRMusicAcademy.Player;
using UnityEngine;

namespace ChangeLab.VRMusicAcademy
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private WaypointData[] waypointDatas;

        [SerializeField] private NPCController npcController;
        [SerializeField] private FPSController playerController;
        private int wayPointIndex = 0;
        private WaypointData currentWayPoint => waypointDatas[wayPointIndex];
        void Start()
        {
            StartNext();
            playerController.StartGame( npcController);
            playerController.playNext += StartNext;
        }

        private void StartNext()
        {
            npcController.StartMovement(currentWayPoint.transform.position, currentWayPoint.audioClip);
            wayPointIndex++;
            if(wayPointIndex == waypointDatas.Length)
                playerController.playNext -= StartNext;
        }

        void OnDestroy()
        {
            if(waypointDatas.Length < wayPointIndex)
                playerController.playNext -= StartNext;
        }
    }

}
