﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnTap : MonoBehaviour
{
    public UnityEvent OnTapped;

    private bool tapReleased = true;
    private EventSystem eventSystem;

    void Start() 
    {
        eventSystem = EventSystem.current;
    }
	
	// Update is called once per frame
	void Update () {
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

    private bool Tapped()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).tapCount > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            // Check if tap was on UI or without a release
            return !FallManager.DidFall && !eventSystem.IsPointerOverGameObject() && tapReleased && PauseMenu.CurrentStatus == PauseMenu.Status.Inactive;
        }
        return false;
    }

}
