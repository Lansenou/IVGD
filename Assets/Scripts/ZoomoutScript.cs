using UnityEngine;

namespace Assets.Scripts
{
    public class ZoomoutScript : MonoBehaviour
    {

        public GameObject MainCamera;
        public GameObject IsometricCamera;
        public GameObject Spawner;
        public GameObject CamToggleButton;

        public void SwitchCams()
        {
            //Spawner.GetComponent<OnTap>().setDisableControls(!MainCamera.active);
            MainCamera.SetActive(!MainCamera.activeSelf);
            IsometricCamera.SetActive(!IsometricCamera.activeSelf);
            CamToggleButton.SetActive(!IsometricCamera.activeSelf);
        }
    }
}
