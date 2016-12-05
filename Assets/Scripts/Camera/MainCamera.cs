using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class MainCamera : Singleton<MainCamera>
    {
        [SerializeField]
        private bool debug;

        [SerializeField]
        private Transform lookTarget;
        
        private float distanceToTarget;

        private void OnDrawGizmos()
        {
            if (debug)
            {
                
            }
        }

        private void Start()
        {
            SetDistanceToTarget();
        }

        void LateUpdate()
        {
            float pinchAmount = 0;
            Quaternion desiredRotation = transform.rotation;

            DetectTouchMovement.Calculate();

            if (Mathf.Abs(DetectTouchMovement.pinchDistanceDelta) > 0)
            { // zoom
                pinchAmount = DetectTouchMovement.pinchDistanceDelta;
            }

            if (Mathf.Abs(DetectTouchMovement.turnAngleDelta) > 0)
            { // rotate
                Vector3 rotationDeg = Vector3.zero;
                rotationDeg.z = -DetectTouchMovement.turnAngleDelta;
                desiredRotation *= Quaternion.Euler(rotationDeg);
            }

            float angle = DetectTouchMovement.turnAngleDelta;
            Debug.Log(angle);

            RotateCamera(angle);
            // not so sure those will work:
//            transform.rotation = desiredRotation;
//            transform.position += Vector3.forward * pinchAmount;
        }

        public void RotateCamera(float angle)
        {
            transform.RotateAround(lookTarget.position, Vector3.up, angle);
        }

        private void SetDistanceToTarget()
        {
            distanceToTarget = Vector3.Distance(transform.position, lookTarget.position);
        }
    }
}
