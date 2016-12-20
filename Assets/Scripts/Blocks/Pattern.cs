using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pattern
{
    [SerializeField]
    private List<Cycle> CycleList = new List<Cycle>();
    private int index = 0;

    public Cycle GetRandomCycle()
	{
        index = (index + Random.Range(1, CycleList.Count)) % CycleList.Count;
        return CycleList [index];
    }
}

[System.Serializable]
public class Target
{
    public Vector3 TargetPos;
}

