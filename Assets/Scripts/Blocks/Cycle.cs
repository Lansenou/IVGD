using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(menuName = "Cycle")]
public class Cycle : ScriptableObject
{
    public Color Color = new Color(1, 1, 1);
    public List<Target> Cycles = new List<Target>();

    private int index;

    public Cycle Reset()
    {
        index = 0;
        return this;
    }

    public Target GetCurrentTarget()
    {
        return Cycles[index];
    }

    public void IncreaseIndex()
    {
        index = (index + 1) % Cycles.Count;
    }
}
