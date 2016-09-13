using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Pattern {
    public List<Cycle> CycleList = new List<Cycle>();

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
}

[System.Serializable]
public class Cycle {
    public Direction CycleType;
    public float MovementTime = 1;
    public Vector3 TargetPos;

    public enum Direction {
        ForwardX,
        ForwardY,
        BaseX,
        BackwardX,
        BackwardY,
        BaseYs
    }
}

