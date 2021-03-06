﻿using System.Collections;
using UnityEngine;
using UnityStandardAssets.Utility;

public class FallManager : MonoBehaviour
{
    public static bool DidFall;

    [SerializeField]
    private int WaitForSeconds = 5;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private SmoothFollow cameraFollow;

    private bool hasFallen;

    private void Start()
    {
        DidFall = false;
        Time.timeScale = 1;
        PauseMenu.CurrentStatus = PauseMenu.Status.Inactive;
    }

    // Update is called once per frame
    private void Update()
    {
        if (hasFallen != DidFall)
        {
            StartCoroutine(EnableTarget(hasFallen = DidFall));
            cameraFollow.SetGameOverCam();
        }
    }

    private IEnumerator EnableTarget(bool active)
    {
        yield return new WaitForSeconds(WaitForSeconds);
        target.SetActive(active);
        FindObjectOfType<BuildingTracker>().AddStackScore();
    }
}