using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pattern
{

    [SerializeField]
    private List<Cycle> CycleList = new List<Cycle>();

    public Cycle GetRandomCycle()
	{
		return CycleList[Random.Range(0, CycleList.Count)].Reset();
    }
}

[System.Serializable]
public class Target
{
    public Vector3 TargetPos;
}

