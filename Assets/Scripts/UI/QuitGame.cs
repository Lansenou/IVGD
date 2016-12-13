using UnityEngine;

public class QuitGame : MonoBehaviour 
{
    [SerializeField]
    private KeyCode QuitButton = KeyCode.Escape;
    [SerializeField]
    private GameObject Target;
    private bool pressed = false;
	
	// Update is called once per frame
	private void Update () {
        if (Input.GetKeyDown(QuitButton)) 
        {
            pressed = !pressed;
            Target.SetActive(pressed);
        }
	}

    public void Quit()
    {
        Application.Quit();
    }

    public void CancelQuit()
    {
        pressed = false;
        Target.SetActive(false);
    }

    public void OnApplicationQuit()
    {
        if (!pressed) {
            Application.CancelQuit();
            pressed = true;
            Target.SetActive(true);
        } 
    }
}
