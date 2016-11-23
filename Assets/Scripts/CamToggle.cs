using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class CamToggle : MonoBehaviour
{
    public GameObject Spawner;
    public GameObject ZoomoutButton;
    public GameObject MainCamera;
    [HideInInspector]
    private bool CamScrollingEnabled = false;

    [HideInInspector] private Button icon;

	// Use this for initialization
	void Start () {
        icon = gameObject.GetComponent<Button>();
    }

    public void ToggleCams()
    {
        Spawner.GetComponent<OnTap>().setDisableControls(!CamScrollingEnabled);
        CamScrollingEnabled = !CamScrollingEnabled;
        ZoomoutButton.SetActive(CamScrollingEnabled);
        MainCamera.GetComponent<SmoothFollow>().SetDraggableCamera(CamScrollingEnabled);

        ColorBlock colorBlock = icon.colors;
        if (CamScrollingEnabled)
        {
            colorBlock.normalColor = Color.white;
            colorBlock.normalColor = new Color(170, 245, 255);
        }
        else
        {
            colorBlock.normalColor = new Color(170, 245, 255);
            colorBlock.normalColor = new Color(170, 245, 255);
        }
        icon.colors = colorBlock;

    }
}
