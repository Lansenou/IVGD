using System.Collections;
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
    [SerializeField]
    private RectTransform scoreText;
    [SerializeField]
    private RectTransform highscoreText;

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
            cameraFollow.setGameOverCam();
        }
    }

    private IEnumerator EnableTarget(bool active)
    {
        yield return new WaitForSeconds(WaitForSeconds);
        target.SetActive(active);
        //set score gui in the right place for gameover screen.
        scoreText.localPosition = new Vector3(0f, -420, 0f);
        highscoreText.localPosition = new Vector3(0f, -500, 0f);
    }
}