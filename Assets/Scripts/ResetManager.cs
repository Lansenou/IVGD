using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ResetManager : MonoBehaviour
{
    private static List<IResettable> Resettables = new List<IResettable>();
    private static ResetManager instance;

    public static ResetManager Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        Resettables = InterfaceHelper.FindObjects<IResettable>().ToList();
    }
	
	// Update is called once per frame
	public void Reset ()
	{
	    foreach (var resettable in Resettables)
	    {
            resettable.Reset();
        }
	}
}
