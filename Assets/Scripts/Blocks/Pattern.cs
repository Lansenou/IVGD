using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pattern
{
    [SerializeField]
    private List<Cycle> CycleList = new List<Cycle>();

    public Cycle GetRandomCycle()
	{
		int index = 0;
		index = (index + Random.Range(0, CycleList.Count)) % CycleList.Count;
		return CycleList [index];
    }
}

[System.Serializable]
public class Target
{
    public Vector3 TargetPos;
}

