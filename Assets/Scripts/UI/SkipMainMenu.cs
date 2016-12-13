using UnityEngine;

public class SkipMainMenu : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Button playButton;
    private static bool hasPlayedBefore = false;

    private void Awake()
    {
        if (hasPlayedBefore)
        {
            playButton.onClick.Invoke();
        }
        else
        {
            hasPlayedBefore = true;
        }
    }
}
