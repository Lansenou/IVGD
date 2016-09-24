using System.Collections;
using UnityEngine;

public class QuitGame : MonoBehaviour {

    public KeyCode QuitButton = KeyCode.Escape;
    public float CooldownTime = 0.2f;
    public GameObject Target;

    private bool pressed = false;
	
	// Update is called once per frame
	void Update () {
        if (!pressed)
        {
            if (Input.GetKeyDown(QuitButton))
            {
                pressed = true;
                Target.SetActive(true);
                StartCoroutine(Quit());
            }
        }
	}

    private IEnumerator Quit()
    {
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime < CooldownTime)
        {
            if (Input.GetKeyDown(QuitButton))
            {
                Application.Quit();
            }
            yield return null;
        }
        pressed = false;
        Target.SetActive(false);
    }



    void OnApplicationQuit()
    {
        if (!pressed)
        {
            Application.CancelQuit();
            pressed = true;
            Target.SetActive(true);
            StartCoroutine(Quit());
        }
    }
}
