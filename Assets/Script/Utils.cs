using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangeLab.ArtGallery
{
    public static class Utils
    {
        public static bool IsInRange(Transform source, Transform target, float range = 80f)
        {
            Vector3 direction = (target.position - source.position).normalized;
            float dot = Vector3.Dot(source.forward, direction);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            return angle < range ;
        }

        public static float GetTime(Vector3 initialPosition, Vector3 finalPosition, float speed)
        {
            if (speed <= 0f)
            {
                throw new System.ArgumentException("Speed must be greater than zero.");
            }

            float distance = Vector3.Distance(initialPosition, finalPosition);
            float time = distance / speed;
            return time;
        }
    }
}
