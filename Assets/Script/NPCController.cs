using System;
using UnityEngine;

namespace ChangeLab.ArtGallery.Player
{
    public partial class NPCController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float rightOffset;
        [SerializeField] private float distance = 3f;
        [field: SerializeField, Range(0, 180)] public float range { get; private set; } = 60f;
        [field: SerializeField] public Transform poistionObj { get; private set; }

        public Transform player { private get; set; }
        private bool playAudio = false;
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
                poistionObj.position = transform.position;
                if (tempTime > time)
                {
                    animator.SetBool(WalkAnimation, false);
                }
            }
            else
            {
                Vector3 direction = player.position - transform.position + transform.right * rightOffset;
                direction = new Vector3(direction.x, 0, direction.z);
                transform.rotation = Quaternion.LookRotation(direction);
            }
            AudioUpdate();
        }

        private void StopMessage()
        {
            if (!playAudio) return;
            playAudio = false;
            audioSource.Stop();
            EnablePlay();
        }

        public void StartMovement(Vector3 finalPosition)
        {
            this.finalPosition = finalPosition;
            this.initialPosition = transform.position;
            CanGetAmplitude = true;
            PlayAudio();
            transform.LookAt(finalPosition);
            animator.SetBool(IdleAnimation, false);
            if (initialPosition == finalPosition) return;
            animator.SetBool(WalkAnimation, true);
            tempTime = 0;
            this.time = Utils.GetTime(initialPosition, finalPosition, 1);
        }

        private void PlayAudio()
        {
            playAudio = true;
            // audioSource.clip = audioClip;
            Invoke(nameof(EnablePlay), audioSource.clip.length);
            audioSource.Play();
        }

        private void EnablePlay()
        {
            animator.SetBool(IdleAnimation, true);
            CanGetAmplitude = false;
            CanInteract?.Invoke();
        }
    }
}
