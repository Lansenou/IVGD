using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnInfo {
        public Transform Folder;
        public GameObject Prefab;
        public BlockColor Color;

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

        // Spawn Gameobject
        GameObject go = Instantiate(Info.Prefab, Info.Folder, false) as GameObject;
        go.transform.position = transform.position;
        go.GetComponent<Renderer>().material.color = Info.Color.Gradient.Evaluate(currentTime);
    }
}