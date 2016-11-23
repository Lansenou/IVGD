using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Blocks;
using Assets.Scripts.Utility;

public class BuildingTracker : MonoBehaviour {

    [SerializeField]
    private Transform summaryParent;
    [SerializeField]
    private BlockSpawner blockSpawner;
    [SerializeField]
    private UI.BuildingInfo summaryPrefab;
    private Dictionary<string, List<Building>> buildings = new Dictionary<string, List<Building>>();
    private List<UI.BuildingInfo> info = new List<UI.BuildingInfo>();

    public void AddBuilding(Destroyable destroyable)
    {
        AddBuilding (new Building (destroyable));
    }

    public void AddBuilding(Building building) {
        if(!buildings.ContainsKey (building.name))
        {
            buildings [building.name] = new List<Building>();  
        }

        AddToInfo(building);
    }

    public void AddStackScore()
    {
        UI.BuildingInfo newBuilding = Instantiate(summaryPrefab);
        newBuilding.transform.SetParent(summaryParent, false);
        newBuilding.transform.SetAsFirstSibling();

        newBuilding.nameText.text = "Tower";
        newBuilding.amountText.text = blockSpawner.GetBlockCount().ToString();
        Metrics.Instance().GetBlockAmount(blockSpawner.GetBlockCount());
        Metrics.Instance().PostAnalytics();
        newBuilding.scoreText.text = HighScore.CurrentScore.ToString("0");
        info.Add(newBuilding);
    }

    private void AddToInfo(Building building)
    {
        buildings[building.name].Add(building);

        PopupController.CreateFloatingText(string.Format("{0} +{1}", building.name, building.score), Color.yellow, 20);

        // If already exists add score to current info
        for (int i = 0; i < info.Count; i++)
        {
            if (info[i].nameText.text == building.name)
            {
                info[i].amountText.text = buildings[building.name].Count.ToString();

                int score = 0;
                for (int j = 0; j < buildings[building.name].Count; j++)
                {
                    score += buildings[building.name][j].score;
                }

                info[i].scoreText.text = score.ToString();
                return;
            }
        }

        // Create info
        CreateInfo(building);
    }

    private void CreateInfo(Building building)
    {
        UI.BuildingInfo newBuilding = Instantiate(summaryPrefab);
        newBuilding.transform.SetParent(summaryParent, false);
        newBuilding.nameText.text = building.name;
        newBuilding.amountText.text = "1";
        newBuilding.scoreText.text = Mathf.FloorToInt(building.score).ToString();
        info.Add(newBuilding);
    }

    public class Building
    {
        public string name;
        public int score;
        public Building(Destroyable destroyable)
        {
            this.name = destroyable.buildingName;
            this.score = destroyable.score;
        }
    }
}
