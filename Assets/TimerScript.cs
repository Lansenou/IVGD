using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Blocks;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public GameObject Spawner;
    float TimeLeft = 10f; 
    private Text timerText;

    public Color WarningColor;

    void Start()
    {
        timerText = GetComponent<Text>();
    }

    void Update()
    {
        TimeLeft -= Time.deltaTime;
        if (TimeLeft <= 3)
        {
            timerText.color = WarningColor;
        }
        else
        {
            timerText.color = Color.white;
        }
        if (TimeLeft <= 0 && !FallManager.DidFall)
        {
            TimeLeft = 1.00f;
            Spawner.GetComponent<Movement>().MoveUp();
            Spawner.GetComponent<BlockSpawner>().Spawn();
        }
        timerText.text = "" + System.Math.Round(TimeLeft, 2);
    }

    public void SetTime(float time)
    {
        TimeLeft = time;
    }
}



