using System;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class MainCamera : Singleton<MainCamera>
    {
        [Header("Camera behaviour")]
        [SerializeField]
        private Transform lookTarget;

        [SerializeField]
        private bool inverseRotation = false;

        [SerializeField]
        private bool inverseZoom = false;

        [SerializeField]
        private float minDistance = 10f;

        [SerializeField]
        private float maxDistance = 30f;

        [Header("Revert behaviour")]
        [SerializeField]
        private float timeTillRevert = 1f;

        [SerializeField]
        private float revertRotationSpeed = 0.2f;

        [SerializeField]
        private float revertZoomSpeed = 0.1f;

        private bool hasMoved = false;
        private float reverseRotationThreshold = 4f;
        private float reverseZoomThreshold = 4f;
        private float timeSinceAdjust;
        private float startRotation;
        private Vector3 startPosition;

        private void Start()
        {
            StorePositionRotation();
        }

        private void LateUpdate()
        {
            RevertCamera();
        }

        public void UpdateCamera(float rotateAmount, float zoomAmount)
        {
            CameraAdjusted();

            RotateCamera(rotateAmount);
            ZoomCamera(zoomAmount);
        }

        private void RotateCamera(float angle)
        {
            if (inverseRotation)
                angle *= -1;

            transform.RotateAround(lookTarget.position, Vector3.up, angle);
        }

        private void ZoomCamera(float amount)
        {
            amount = Mathf.Clamp(amount, -1, 1f);

            if (inverseZoom)
                amount *= -1;

            transform.position += transform.forward * amount;
        }

        private void CameraAdjusted()
        {
            hasMoved = true;
            timeSinceAdjust = Time.time;
        }

        private void StorePositionRotation()
        {
            startPosition = transform.position;
            startRotation = transform.rotation.y;
        }

        private void RevertCamera()
        {
            Debug.Log(GetRotationAngle());
            if (!CanRevert()) return;

            if (HasRotated())
            {
                RevertRotation();
            }

            if (HasZoomed())
            {
                RevertZoom();
            }

            hasMoved = HasRotated() || HasZoomed();
        }

        private void RevertRotation()
        {
            float direction = revertRotationSpeed * Time.deltaTime;
            direction = transform.rotation.y < startRotation ? direction : direction * -1; 
            transform.RotateAround(lookTarget.position, Vector3.up, direction);
        }

        private void RevertZoom()
        {
            float amount = revertZoomSpeed;
            amount *= Vector3.Distance(transform.position, startPosition);
            transform.position += (transform.forward * amount * Time.deltaTime);
        }

        private bool CanRevert()
        {
            return hasMoved && Time.time > (timeSinceAdjust + timeTillRevert);
        }

        private bool HasRotated()
        {
            return GetRotationAngle() > reverseRotationThreshold;
        }

        private bool HasZoomed()
        {
            return Vector3.Distance(transform.position, lookTarget.position) - reverseZoomThreshold
                   > Vector3.Distance(lookTarget.position, startPosition);
        }

        private float GetRotationAngle()
        {

            //bug When the camera is zoomed out it increases the angle as well causing HasRotated() to 
            //bug return true even when the y rotation has be reverted to it's original value. 
            Vector3 position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
            return Vector3.Angle(position, startPosition);
        }
    }
}