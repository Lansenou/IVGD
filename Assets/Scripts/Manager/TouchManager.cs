using Assets.Scripts.Camera;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class TouchManager : Singleton<TouchManager>
    {
        [SerializeField]
        private float tapTimeThreshold = 1f;

        [SerializeField]
        private float maxRotationSpeed = 5f;

        [SerializeField]
        private float zoomMultiplier = 4f;

        private float touchTime;

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
                        OnBegan();
                        break;
                    case TouchPhase.Moved:
                        OnMove(touch1);
                        break;
                    case TouchPhase.Ended:
                        OnRelease(touch1);
                        break;
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];
                
                if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
                {
                    OnMove(touch1, touch2);
                }
            }
        }

        private void OnBegan()
        {
            touchTime = Time.time;
        }

        private void OnMove(Touch touch)
        {
            Slide(touch);
        }

        private void OnMove(Touch touch1, Touch touch2)
        {
            Zoom(touch1, touch2);
        }

        private void OnRelease(Touch touch)
        {
            if (Time.time < touchTime + tapTimeThreshold)
                Tap();
        }

        private void Slide(Touch touch)
        {
            MainCamera.Instance.UpdateCamera(touch.deltaPosition.x, 0);
        }

        private void Tap()
        {
            // spawn a block
        }

        private void Zoom(Touch touch1, Touch touch2)
        {
            float zoomAmount = (touch1.deltaPosition.y + touch2.deltaPosition.y) / 2;
            zoomAmount *= zoomMultiplier;
            zoomAmount = Mathf.Clamp(zoomAmount, 0, int.MaxValue);
            MainCamera.Instance.UpdateCamera(0, zoomAmount);
        }
    }
}