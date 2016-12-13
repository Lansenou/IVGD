using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Blocks;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Gradient GradientColor = new Gradient();
    public GameObject Spawner;
    public Text GetText() { return timerText; }

    private float timeLeft, totalTime = 10f;
    private Text timerText;    

    void Start()
    {
        timerText = GetComponent<Text>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.color = GradientColor.Evaluate(1-timeLeft / totalTime);

        if (timeLeft <= 0 && !FallManager.DidFall && !OnTap.towerFalling)
        {            
            timeLeft = 1.00f;
            Spawner.GetComponent<Movement>().MoveUp();
            Spawner.GetComponent<BlockSpawner>().Spawn();
        }
        timerText.text = timeLeft.ToString("0.0");
    }

    public void SetTime(float time)
    {
        timeLeft = totalTime = time;
    }
}



