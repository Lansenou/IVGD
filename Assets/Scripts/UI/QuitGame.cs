using System.Collections;
using UnityEngine;

public class QuitGame : MonoBehaviour 
{
    public KeyCode QuitButton = KeyCode.Escape;
    public float CooldownTime = 0.2f;
    public GameObject Target;

    private bool pressed = false;
    private bool secondPress = false;
	
	// Update is called once per frame
	private void Update () {
        if (!pressed && Input.GetKeyDown(QuitButton)) 
        {
            pressed = true;
            Target.SetActive(true);
            StartCoroutine(Quit());
        } 
        else if (pressed && Input.GetKeyDown(QuitButton))
        {
            secondPress = true;
        }
	}

    private IEnumerator Quit()
    {
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime < CooldownTime)
        {
            if (secondPress)
            {
                Application.Quit();
            }
            yield return null;
        }
        pressed = false;
        Target.SetActive(false);
    }

    public void OnApplicationQuit()
    {
        if (!pressed) {
            Application.CancelQuit();
            pressed = true;
            Target.SetActive(true);
            StartCoroutine(Quit());
        } 
        else
        {
            Application.Quit();
        }
    }
}
