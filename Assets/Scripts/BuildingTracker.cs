using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BuildingTracker : MonoBehaviour {

    [SerializeField]
    private Text summary;

    public class Building {
        public string name;
        public int score;
        public Building(Destroyable destroyable) {
            this.name = destroyable.buildingName;
            this.score = destroyable.score;
        }
    }

   // private Dictionary<string, int> buildings = new Dictionary<string, int>();
	// Use this for initialization
    private Dictionary<string, List<Building>> buildings = new Dictionary<string, List<Building>>();

    public void AddBuilding(Destroyable destroyable)
    {
        AddBuilding (new Building (destroyable));
    }

    public void AddBuilding(Building building) {
        if(!buildings.ContainsKey (building.name))
        {
            buildings [building.name] = new List<Building>();  
        }
        buildings [building.name].Add (building);
    }

    public void ShowSummary()
    {
        foreach (KeyValuePair<string, List<Building>> kvp in buildings)
        {
            int score = 0;
            foreach(Building building in kvp.Value)
            {
                score += building.score;
            }
            //Debug.Log("Name: " + kvp.Key + " Score:" + score + " Count: " + kvp.Value.Count);
            summary.text = summary.text + "\n" + "Name: " + kvp.Key + " Score:" + score + " Count: " + kvp.Value.Count;
        }

    }


}
