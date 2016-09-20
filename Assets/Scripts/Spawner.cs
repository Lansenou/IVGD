using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnInfo {
        public Transform Folder;
        public GameObject Prefab;
        public List<GameObject> prefabs;
        public BlockColor Color;
        public int selectedPrefab = 0;

        [System.Serializable]
        public class BlockColor {
            public Gradient Gradient;
            public float ColorSpeed = 1;
        }
    }

    public SpawnInfo Info;
    private float currentTime = 0;

    public void Spawn()
    {
        currentTime = (currentTime * Time.deltaTime * Info.Color.ColorSpeed) % 1;

        GameObject go = Instantiate (Info.prefabs [Info.selectedPrefab], Info.Folder, false) as GameObject;
        go.transform.position = transform.position;
        go.GetComponent<Renderer>().material.color = Info.Color.Gradient.Evaluate(currentTime);
    }
}