using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Pattern {
    public List<Cycle> CycleList = new List<Cycle>();
    public List<Cycle> CycleList2 = new List<Cycle>();
    public List<Cycle> CycleList3 = new List<Cycle>();
    public List<Cycle> CycleList4 = new List<Cycle>();

    [HideInInspector]
    public List<Cycle> CycleList1 = new List<Cycle>();
    

    public Cycle GetCycleInfo(Cycle.Direction cycle) {
        return CycleList.First(i => i.CycleType == cycle);
    }

    public Cycle GetNextCycle(Cycle cycle) {
        for (int i = 0; i < CycleList.Count; i++) {
            if (CycleList[i] == cycle)
                return CycleList[(i + 1) % CycleList.Count];
        }

        Debug.LogError("No next cycle found");
        return CycleList[0];
    }

    public void SwitchCycles()
    {
        if (CycleList1.Count == 0)
        {
            CycleList1 = CycleList;
            Debug.LogWarning("cycle list set");
        }
        System.Random randomizer = new System.Random();
        int randomNum = randomizer.Next(4);
        switch (randomNum)
        {
            case 0:
                CycleList = CycleList1;
                Debug.LogWarning("cycle list 1!");
                break;
            case 1:
                CycleList = CycleList2;
                Debug.LogWarning("cycle list 2!");
                break;
            case 2:
                CycleList = CycleList3;
                Debug.LogWarning("cycle list 3!");
                break;
            case 3:
                CycleList = CycleList4;
                Debug.LogWarning("cycle list 4!");
                break;
        }
    }
}

[System.Serializable]
public class Cycle {
    public Direction CycleType;
    public Vector3 TargetPos;
    public Color Color;

    public enum Direction
    {
        First,
        Second,
        Third,
        Fourth,
        Fith,
        Sixth,
        Seventh,
        Eighth,
        Ninth,
        Tenth
    }

}

