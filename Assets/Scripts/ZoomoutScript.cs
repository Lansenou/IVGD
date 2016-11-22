using UnityEngine;

namespace Assets.Scripts
{
    public class ZoomoutScript : MonoBehaviour
    {

        public GameObject MainCamera;
        public GameObject IsometricCamera;
        public GameObject Spawner;
        public GameObject CamToggleButton;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SwitchCams()
        {
            //Spawner.GetComponent<OnTap>().setDisableControls(!MainCamera.active);
            MainCamera.SetActive(!MainCamera.active);
            IsometricCamera.SetActive(!IsometricCamera.active);
            CamToggleButton.SetActive(!IsometricCamera.active);
        }
    }
}
