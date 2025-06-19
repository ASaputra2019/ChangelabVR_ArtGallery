using System;
using UnityEngine;

namespace ChangeLab.VRMusicAcademy.Player
{
    public partial class NPCController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Transform player;
        private float tempTime = 0;
        private float time = 0;
        private Vector3 initialPosition;
        private Vector3 finalPosition;
        public event Action CanInteract;
        private const string IdleAnimation = "IsIdle";
        private const string WalkAnimation = "IsWalking";
        void Update()
        {
            if (tempTime < time)
            {
                tempTime += Time.deltaTime;
                transform.position = Vector3.Lerp(initialPosition, finalPosition, tempTime / time);
                if (tempTime > time)
                {
                    animator.SetBool(WalkAnimation, false);
                }
            }
            else
            {
                Vector3 direction = player.position - transform.position;
                direction = new Vector3(direction.x, 0, direction.z);
                transform.rotation = Quaternion.LookRotation(direction);
            }

             AudioUpdate();
        }

        public void StartMovement(Vector3 finalPosition, AudioClip audioClip)
        {
            this.finalPosition = finalPosition;
            this.initialPosition = transform.position;
            Invoke(nameof(EnablePlay), audioClip.length);
            animator.SetBool(WalkAnimation, true);
            animator.SetBool(IdleAnimation, false);
            PlayAudio(audioClip);
            tempTime = 0;
            this.time = Utils.GetTime(initialPosition, finalPosition, 1);
            transform.LookAt(finalPosition);
        }

        private void PlayAudio(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        private void EnablePlay()
        {
            animator.SetBool(IdleAnimation, true);
            CanInteract?.Invoke();
        }
    }
}
