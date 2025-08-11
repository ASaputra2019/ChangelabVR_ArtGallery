using UnityEngine;
using UnityEditor;
namespace ChangeLab.ArtGallery.Recorder
{
    public class Recorder : MonoBehaviour
    {
        [SerializeField] private Transform camObj;
        [SerializeField] private float speed = 1f;
        public float arrivalThreshold = 0.01f;
        private Transform pointB;
        int index = 0;
        private EachRecorderPoint eachRecorderPoint;
        private bool hasArrived = false;
        [SerializeField] private bool runGame = false;
        private bool pause = false;
        void Start()
        {
            pointB = transform.GetChild(0);
            // #if UNITY_EDITOR
            // AutoStartRecorder.StartRecording();
            //     #endif
        }
        void Update()
        {
            if (!runGame || pause) return;

            if (hasArrived)
            {
                UpdatePosition();
            }

            // Move toward pointB
            camObj.position = Vector3.MoveTowards(camObj.position, pointB.position, speed * Time.deltaTime);

            // Smoothly rotate to face pointB
            // Vector3 direction = pointB.position - camObj.position;
            // if (direction.sqrMagnitude > 0.001f) // avoid zero direction
            // {
            //     Quaternion targetRotation = Quaternion.LookRotation(direction);
            //     camObj.rotation = Quaternion.Slerp(camObj.rotation, targetRotation, Time.deltaTime * 5f); // 5f = smoothness factor
            // }

            // Check if arrived
            if (Vector3.Distance(camObj.position, pointB.position) <= arrivalThreshold)
            {
                hasArrived = true;
            }
        }


        private void UpdatePosition()
        {
            if (index == transform.childCount)
            {
                #if UNITY_EDITOR
                //Stop Recording
                EditorApplication.isPlaying = false;
                #endif
                runGame = false;
                return;
            }
            Transform pointB = transform.GetChild(index);
            if (pointB.TryGetComponent<EachRecorderPoint>(out var eachRecorderPoint))
            {
                this.eachRecorderPoint = eachRecorderPoint;
                eachRecorderPoint.StartAudio();
                eachRecorderPoint.startMovement += StartMovement;
                pause = true;
            }
            else
            {
                StartMovement();
            }
            this.pointB = pointB;
            index++;
        }

        private void StartMovement()
        {
            hasArrived = false;
            pause = false;
            if (eachRecorderPoint)
                eachRecorderPoint.startMovement -= StartMovement;
        }
    }
}
