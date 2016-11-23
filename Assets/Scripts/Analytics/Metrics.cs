using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System;

public class Metrics : MonoBehaviour
{

    /*
        This class provides an instance to make use of the Unity Analytics.
        Can't use CustomEvents in awake, onapplicationquit, start, etc.
        https://docs.unity3d.com/ScriptReference/Analytics.Analytics.CustomEvent.html
    */
    private static Metrics instance;
    private float playTime;
    private DateTime dateTime;
    private int blockAmount;

    public static Metrics Instance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PostAnalytics()
    {
        LogPlayTime();
        LogDateTime();

        Analytics.CustomEvent("Blocks", new Dictionary<string, object>
        {
            {"Block amount", blockAmount },
            {"Session time", playTime },
            {"Date time", dateTime }
        });
    }

    public void GetBlockAmount(int blockCount)
    {
        blockAmount = blockCount;
    }

    private void LogPlayTime()
    {
        playTime = Time.time / 60;
    }

    private void LogDateTime()
    {
        dateTime = DateTime.Now;
    }

}
