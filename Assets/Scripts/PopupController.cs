using UnityEngine;
using System.Collections;

public class PopupController : MonoBehaviour
{

    private static FloatingPopupText popupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("canvas");
        if (!popupText)
            popupText = Resources.Load<FloatingPopupText>("Prefabs/PopupTextParent");
    }

    public static void CreateFloatingText(string text)
    {
        FloatingPopupText instance = Instantiate(popupText);
        instance.transform.SetParent(canvas.transform, false);
        instance.setText(text);
    }
}
