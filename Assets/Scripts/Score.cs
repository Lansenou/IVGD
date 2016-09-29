using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Distance distance;
    [SerializeField] private Multiplier multiplier;
    [SerializeField] private Text multiplierText;
    [SerializeField] private bool useAverageList = false;

    private Vector3 previousBlock;
    private float score = 0;
    private float placementScore = 1;
    private List<Transform> averageList = new List<Transform>();

    public enum Block {
        Perfect,
        Good,
        Ok,
        Bad
    }

    public void AddPoints(Transform addedBlock)
    {
        if (useAverageList)
        {
            // Aggregrate is the sum
            Vector3 average = averageList.Aggregate(Vector3.zero, (current, t) => current + t.transform.position) / averageList.Count;
            Vector3 newBlock = new Vector3(addedBlock.position.x, 0, addedBlock.position.z);
            average.y = 0;
            Block grade = distance.GradeDistance(Vector3.Distance(average, newBlock));
            score += placementScore * multiplier.AddMultiplier(grade);
            averageList.Add(addedBlock);
        } 
        else 
        {
            Vector3 newBlock = new Vector3(addedBlock.position.x, 0, addedBlock.position.z);
            Block grade = distance.GradeDistance(Vector3.Distance(previousBlock, newBlock));
            score += placementScore * multiplier.AddMultiplier(grade);
            previousBlock = newBlock;
        }


        // Set text
        HighScore.CurrentScore = score;
        multiplierText.text = "Multiplier: " + multiplier.GetMultiplier().ToString("0.00");

    }

    [System.Serializable]
    public class Distance
    {
        public float PerfectDistance = 0.1f;
        public float GoodDistance = 0.2f;
        public float OkDistance = 0.5f;

        public Block GradeDistance(float distance)
        {
            Block grade = Block.Bad;
            if (distance < PerfectDistance)
            {
                grade = Block.Perfect;
            } 
            else if (distance < GoodDistance) 
            {
                grade = Block.Good;
            } 
            else if (distance < OkDistance)
            {
                grade = Block.Ok;
            }
            return grade;
        } 
    }

    [System.Serializable]
    public class Multiplier {
        public float Perfect = 0.1f;
        public float Good = 0.05f;
        public float Ok = 0.025f;

        private float multiplier = 1;

        public float GetMultiplier()
        {
            return multiplier;
        }

        public float AddMultiplier(Block block)
        {
            switch (block)
            {
                case Block.Perfect:
                    multiplier += Perfect;
                    break;
                case Block.Good:
                    multiplier += Good;
                    break;
                case Block.Ok:
                    multiplier += Ok;
                    break;
                case Block.Bad:
                    multiplier = 1;
                    break;
                default:
                    multiplier = 1;
                    Debug.LogError("Case Block." + block.ToString() + " was not handled.");
                    break;
            }
            return multiplier;
        }

    }
}
