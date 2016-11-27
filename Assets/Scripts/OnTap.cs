using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnTap : MonoBehaviour
{
    public UnityEvent OnTapped;

    private bool tapReleased = true;
    private EventSystem eventSystem;

    public bool disableControls = false;

    void Start() 
    {
        eventSystem = EventSystem.current;
    }
	
	// Update is called once per frame
	void Update () {
	    if (!disableControls)
	    {
	        if (Tapped())
	        {
	            tapReleased = false;
	            OnTapped.Invoke();
	        }
	        else
	        {
	            tapReleased = true;
	        }
	    }
	}

    private bool Tapped()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetKeyDown(KeyCode.Space))
        {
            // Check if tap was on UI or without a release
            return !FallManager.DidFall && !eventSystem.IsPointerOverGameObject(0) && tapReleased && PauseMenu.CurrentStatus == PauseMenu.Status.Inactive;
        }
        return false;
    }

    public void setDisableControls(bool boolean)
    {
        disableControls = boolean;
    }

}
