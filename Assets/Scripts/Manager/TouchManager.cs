using Assets.Scripts.Camera;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class TouchManager : Singleton<TouchManager>
    {
        private const float PinchTurnRatio = Mathf.PI / 2;
        private const float MinTurnAngle = 0;

        private const float PinchRatio = 1;
        private const float MinPinchDistance = 0;

        private const float PanRatio = 1;
        private const float MinPanDistance = 0;

        // The delta of the angle between two touch points
        private float TurnAngleDelta;

        // The angle between two touch points
        private float TurnAngle;

        // The delta of the distance between two touch points that were distancing from each other
        private float PinchDistanceDelta;

        // The distance between two touch points that were distancing from each other
        private float PinchDistance;

        [SerializeField]
        private float tapThreshold = 1f;

        private float touchTime = 0f;

        private void LateUpdate()
        {
            TouchInput();
        }

        private void TouchInput()
        {
            if (Input.touchCount == 1)
            {
                Touch touch1 = Input.touches[0];

                switch (touch1.phase)
                {
                    case TouchPhase.Began:
                        StoreTouchTime();
                        break;
                    case TouchPhase.Moved:
                        OnSlide(touch1);
                        break;
                    case TouchPhase.Ended:
                        OnTap(touch1);
                        break;
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];

                if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
                {
                    
                }
            }
        }

        private void StoreTouchTime()
        {
            touchTime = Time.time;
        }

        private void OnTap(Touch touch)
        {
            if (Time.time < touchTime + tapThreshold)
                Debug.Log("tap");
        }

        private void OnSlide(Touch touch)
        {
            Debug.Log("slide");
        }

        private void CalculateTouch()
        {
            PinchDistance = PinchDistanceDelta = 0;
            TurnAngle = TurnAngleDelta = 0;

            // if two fingers are touching the screen at the same time ...
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];

                // ... if at least one of them moved ...
                if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
                {
                    // ... check the delta distance between them ...
                    PinchDistance = Vector2.Distance(touch1.position, touch2.position);
                    float prevDistance = Vector2.Distance(touch1.position - touch1.deltaPosition,
                                                          touch2.position - touch2.deltaPosition);
                    PinchDistanceDelta = PinchDistance - prevDistance;

                    // ... if it's greater than a minimum threshold, it's a pinch!
                    if (Mathf.Abs(PinchDistanceDelta) > MinPinchDistance)
                    {
                        PinchDistanceDelta *= PinchRatio;
                    }
                    else
                    {
                        PinchDistance = PinchDistanceDelta = 0;
                    }

                    // ... or check the delta angle between them ...
                    TurnAngle = Angle(touch1.position, touch2.position);
                    float prevTurn = Angle(touch1.position - touch1.deltaPosition,
                                           touch2.position - touch2.deltaPosition);
                    TurnAngleDelta = Mathf.DeltaAngle(prevTurn, TurnAngle);

                    // ... if it's greater than a minimum threshold, it's a turn!
                    if (Mathf.Abs(TurnAngleDelta) > MinTurnAngle)
                    {
                        TurnAngleDelta *= PinchTurnRatio;
                    }
                    else
                    {
                        TurnAngle = TurnAngleDelta = 0;
                    }
                }

                UpdateCamera();
            }
        }

        private void UpdateCamera()
        {
            float pinchAmount = 0;
            Quaternion desiredRotation = MainCamera.Instance.transform.rotation;

            if (Mathf.Abs(PinchDistanceDelta) > 0)
            {
                // zoom
                pinchAmount = PinchDistanceDelta;
            }

            if (Mathf.Abs(TurnAngleDelta) > 0)
            {
                // rotate
                Vector3 rotationDeg = Vector3.zero;
                rotationDeg.z = -TurnAngleDelta;
                desiredRotation *= Quaternion.Euler(rotationDeg);
            }

            MainCamera.Instance.RotateCamera(TurnAngleDelta);
            MainCamera.Instance.ZoomCamera(pinchAmount);
        }

        private float Angle(Vector2 pos1, Vector2 pos2)
        {
            Vector2 from = pos2 - pos1;
            Vector2 to = new Vector2(1, 0);

            float result = Vector2.Angle(from, to);
            Vector3 cross = Vector3.Cross(from, to);

            if (cross.z > 0)
            {
                result = 360f - result;
            }

            return result;
        }
    }
}