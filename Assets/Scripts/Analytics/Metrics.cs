using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System;
using Assets.Scripts.Utility;
    
public class Metrics : Singleton<Metrics>
{

    /*
        This class provides an instance to make use of the Unity Analytics.
        Can't use CustomEvents in awake, onapplicationquit, start, etc.
        https://docs.unity3d.com/ScriptReference/Analytics.Analytics.CustomEvent.html
    */

    private float playTime;
    private DateTime dateTime;
    private int blockAmount;

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
