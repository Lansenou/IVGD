using UnityEngine;
using System.Collections;

public class PopupController : MonoBehaviour
{

    private static FloatingPopupText popupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        if (!canvas)
        {
            canvas = GameObject.FindGameObjectWithTag("Canvas");
        }
        if (!popupText) { 
            popupText = Resources.Load<FloatingPopupText>("Prefabs/PopupTextParent");
        }
    }

    public static void CreateFloatingText(string text, Color color)
    {
        Initialize();
        FloatingPopupText instance = Instantiate(popupText);
        instance.transform.SetParent(canvas.transform, false);
        instance.setText(text);
        instance.setColor(color);
    }
}
