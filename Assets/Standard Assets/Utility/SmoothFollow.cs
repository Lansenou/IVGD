using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{
	    [SerializeField]
	    public bool DragCamera;

        // The distance in the x-z plane to the target
        [SerializeField]
        private float distance = 10.0f;

        // the height we want the camera to be above the target
        [SerializeField]
        private float height = 5.0f;

        // The target we are following
        [SerializeField]
		private Transform target;
        
	    [SerializeField]
		private float rotationDamping;

		[SerializeField]
		private float heightDamping;

        [SerializeField]
        private Camera targetCamera;

	    [SerializeField]
        private Transform startBlock;

	    public float turnSpeed = 4.0f;      // Speed of camera turning when mouse moves in along an axis
	    public float panSpeed = 4.0f;       // Speed of the camera when being panned
	    public float zoomSpeed = 4.0f;      // Speed of the camera going back and forth
        
	    private bool isPanning;     // Is the camera being panned?
	    private bool isRotating;    // Is the camera being rotated?
	    private bool isZooming;     // Is the camera zooming?
	    private bool camFollowBlock;
	    private float defaultFieldOfView;
	    private Transform lastBlock;
	    private Transform camTarget;
	    private Vector3 startPosition;
	    private Vector3 mouseOrigin;    // Position of cursor when mouse dragging starts

	    // Use this for initialization
        void Start()
	    {
	        startPosition = startBlock.position;
	        defaultFieldOfView = targetCamera.fieldOfView;
	    }

        // Update is called once per frame
        void LateUpdate()
        {

            // Check if tower is falling
            if (Input.GetKey(KeyCode.A) || camFollowBlock) //todo change this to an event when detected that the tower is falling.
            {
                float newFOV = defaultFieldOfView + Vector3.Distance(lastBlock.position, startPosition);
                if (newFOV > 135)
                {
                    newFOV = 135;
                }
                targetCamera.fieldOfView = newFOV;
                camTarget = lastBlock.transform;
            }
            else
            {
                camTarget = target;
            }

            // Early out if we don't have a target
            if (!camTarget)
                return;

            // Calculate the current rotation angles
            var wantedRotationAngle = camTarget.eulerAngles.y;
            var wantedHeight = camTarget.position.y + height;

            var currentRotationAngle = transform.eulerAngles.y;
            var currentHeight = transform.position.y;

            // Damp the rotation around the y-axis
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = camTarget.position;
            transform.position -= currentRotation * Vector3.forward * distance;

            // Set the height of the camera
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            // Always look at the target
            transform.LookAt(camTarget);

            if (DragCamera)
            {
                rotateCamAround();
            }

        }

	    private void rotateCamAround()
	    {
            // Get the left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                // Get mouse origin
                mouseOrigin = Input.mousePosition;
                isRotating = true;
            }

            // Get the right mouse button
            if (Input.GetMouseButtonDown(1))
            {
                // Get mouse origin
                mouseOrigin = Input.mousePosition;
                isPanning = true;
            }

            // Get the middle mouse button
            if (Input.GetMouseButtonDown(2))
            {
                // Get mouse origin
                mouseOrigin = Input.mousePosition;
                isZooming = true;
            }

            // Disable movements on button release
            if (!Input.GetMouseButton(0)) isRotating = false;
            if (!Input.GetMouseButton(1)) isPanning = false;
            if (!Input.GetMouseButton(2)) isZooming = false;

            // Rotate camera along X and Y axis
            if (isRotating)
            {
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

                transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
                transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
            }

            // Move the camera on it's XY plane
            if (isPanning)
            {
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

                Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
                transform.Translate(move, Space.Self);
            }

            // Move the camera linearly along Z axis
            if (isZooming)
            {
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

                Vector3 move = pos.y * zoomSpeed * transform.forward;
                transform.Translate(move, Space.World);
            }
        }

	    public void NewLastBlock(Transform newBlock)
        {
            lastBlock= newBlock;
            target = lastBlock.transform;
        }

	    public void SetGameOverCam()
	    {
	        camFollowBlock = true;
	    }

	    public void SetDraggableCamera(bool Bool)
	    {
	        DragCamera = Bool;
	    }


    }
}