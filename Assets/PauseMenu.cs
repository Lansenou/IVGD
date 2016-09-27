using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public enum Status
    {
        Inactive,
        Active,
        Hiding,
        Showing
    }

    public static Status CurrentStatus = Status.Inactive;

    private Graphic[] graphics;
    private Text[] text;
    private float transitionSpeed = 0.1f;
    private float oldTimeScale = 1;
    private bool initialized = false;

    public void Hide()
    {
        if (CurrentStatus == Status.Active)
        {
            CurrentStatus = Status.Hiding;
            StartCoroutine(Hiding());
        }
    }

    public void Show()
    {
        if (CurrentStatus == Status.Inactive)
        {
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0;
            CurrentStatus = Status.Showing;
            gameObject.SetActive(true);
            StartCoroutine(Showing());
        }
    }

    public IEnumerator Hiding()
    {
        if (CurrentStatus == Status.Hiding)
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].CrossFadeAlpha(0, transitionSpeed, true);
            }

            for (int i = 0; i < text.Length; i++)
            {
                text[i].CrossFadeAlpha(0, transitionSpeed, true);
            }

            yield return new WaitForSecondsRealtime(transitionSpeed);
            gameObject.SetActive(false);
            CurrentStatus = Status.Inactive;
            Time.timeScale = oldTimeScale;
        }
    }

    public IEnumerator Showing()
    {
        if (CurrentStatus == Status.Showing)
        {
            while (!initialized)
            {
                yield return null;
            }
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].CrossFadeAlpha(0, 0, true);
                graphics[i].CrossFadeAlpha(1, transitionSpeed, true);
            }

            for (int i = 0; i < text.Length; i++)
            {
                text[i].CrossFadeAlpha(0, 0, true);
                text[i].CrossFadeAlpha(1, transitionSpeed, true);
            }

            yield return new WaitForSecondsRealtime(transitionSpeed);
            CurrentStatus = Status.Active;
        }
    }

	// Use this for initialization
	private void Start () {
        graphics = GetComponentsInChildren<Image>();
        text = GetComponentsInChildren<Text>();

        // Hide if it's at the start of the game
        if (CurrentStatus != Status.Showing)
        {
            // Force state and inactive menu
            Hide();
            gameObject.SetActive(false);
            CurrentStatus = Status.Inactive;
            Time.timeScale = 1;
        }

        initialized = true;
	}
}
