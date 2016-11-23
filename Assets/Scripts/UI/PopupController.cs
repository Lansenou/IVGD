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

    public static void CreateFloatingText(string text, Color color, float randomOffset = 0)
    {
        Initialize();
        FloatingPopupText instance = Instantiate(popupText);
        instance.transform.SetParent(canvas.transform, false);
        instance.SetText(text);
        instance.SetColor(color);
        instance.transform.position += new Vector3(0, Random.Range(-randomOffset, randomOffset));        
    }
}
