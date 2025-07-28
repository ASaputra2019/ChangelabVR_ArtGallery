using UnityEngine;
using System;
namespace ChangeLab.ArtGallery
{
    public class StopAudio : MonoBehaviour
    {
        public static event Action StopMessage;
        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<FPSController>(out FPSController fPSController))
            {
                StopMessage?.Invoke();
            }
        }
    }
}