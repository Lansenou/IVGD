using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class FallManager : MonoBehaviour
{
    public static bool DidFall;
    public static bool BuildingHasExploded = false;
    public TimerScript Timer;

    [SerializeField]
    private int WaitForSeconds = 5;
    [SerializeField]
    private UnityEvent OnFall;
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
            FindObjectOfType<BuildingTracker>().AddStackScore();
            Timer.enabled = false;
        }
    }

    private IEnumerator EnableTarget(bool active)
    {
        Text text = Timer.GetText();
        text.color = Color.white;
        float currentTime = 0;
        while (currentTime < WaitForSeconds)
        {
            if (BuildingHasExploded)
            {
                BuildingHasExploded = false;
                currentTime = 0;
            }
            currentTime += Time.deltaTime;
            text.text = (WaitForSeconds - currentTime).ToString("0.0");
            yield return null;
        }
        OnFall.Invoke();
    }
}
