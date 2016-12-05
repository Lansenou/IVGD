using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class MainCamera : Singleton<MainCamera>
    {
        [SerializeField]
        private Transform lookTarget;

        private float distanceToTarget;

        private void Start()
        {
            SetDistanceToTarget();
        }

        public void RotateCamera(float angle)
        {
            transform.RotateAround(lookTarget.position, Vector3.up, angle);
        }

        public void ZoomCamera(float amount)
        {
            Debug.Log(amount);
            amount = Mathf.Clamp(amount, -1f, 1f);
            transform.position += transform.forward * amount;
        }

        private void SetDistanceToTarget()
        {
            if (lookTarget != null)
                distanceToTarget = Vector3.Distance(transform.position, lookTarget.position);
        }
    }
}