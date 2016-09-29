using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{

		// The target we are following
		[SerializeField]
		private Transform target;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 10.0f;
		// the height we want the camera to be above the target
		[SerializeField]
		private float height = 5.0f;

		[SerializeField]
		private float rotationDamping;
		[SerializeField]
		private float heightDamping;

        [SerializeField]
        private Camera targetCamera;
        [SerializeField]
        private Transform startBlock;
        [HideInInspector]
        private float defaultFieldOfView;
        [HideInInspector]
        private Rigidbody lastBlock;
        [HideInInspector]
        private Transform camTarget;
        [HideInInspector]
        private bool camFollowBlock;

	    [HideInInspector] private Vector3 startPosition;

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
                targetCamera.fieldOfView = defaultFieldOfView + Vector3.Distance(lastBlock.transform.position, startPosition);
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
        }

        public void NewLastBlock(Rigidbody newBlock)
        {
            lastBlock = newBlock;
        }

	    public void setGameOverCam()
	    {
	        camFollowBlock = true;
	    }

	}
}