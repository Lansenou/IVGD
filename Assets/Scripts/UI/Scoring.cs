using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    [SerializeField] private Distance distance;
    [SerializeField] private Multiplier multiplier;
    [SerializeField] private Text scoreText;
    [SerializeField] private bool useAverageList = true;
    [SerializeField] private Block minimalGradeToSnap = Block.Good;

    [SerializeField]
    private Transform previousBlock = null;
    private float score = 0;
    private float placementScore = 1;
    [SerializeField]
    private List<Transform> averageList = new List<Transform>();

    private static Color blue = Color.cyan;
    private static Color green = Color.green;
    private static Color yellow = Color.yellow;

    public enum Block {
        Bad,
        Ok,
        Good,
        Perfect
    }

    public void AddPoints(Transform addedBlock)
    {
        if (useAverageList)
        {
            GradeList(addedBlock);
        }
        else
        {
            GradePrevious(addedBlock);
        }

        // Update text
        HighScore.instance.CurrentScore = score;
    }

    private void Start()
    {
        HighScore.instance.OnScoreUpdate += (float score) => { scoreText.text = score.ToString("0"); };
    }

    private void GradeList(Transform addedBlock)
    {
        if (averageList.Count > 0)
        {
            Vector3 average = averageList.Aggregate(Vector3.zero, (current, t) => current + t.transform.position) / averageList.Count;
            Vector3 newBlock = new Vector3(addedBlock.position.x, 0, addedBlock.position.z);
            average.y = 0;
            Block grade = distance.GradeDistance(Vector3.Distance(average, newBlock));
            score += placementScore * multiplier.AddMultiplier(grade);
            if (grade >= minimalGradeToSnap)
            {
                SnapBlock(addedBlock, average);
            }
        }
        else
        {
            score += placementScore * multiplier.AddMultiplier(Block.Perfect);
        }
        averageList.Add(addedBlock);
    }

    private void GradePrevious(Transform addedBlock)
    {
        if (previousBlock)
        {
            Vector3 newBlock = new Vector3(addedBlock.position.x, previousBlock.position.y, addedBlock.position.z);
            Block grade = distance.GradeDistance(Vector3.Distance(previousBlock.position, newBlock));
            score += placementScore * multiplier.AddMultiplier(grade);
            if (grade >= minimalGradeToSnap)
            {
                SnapBlock(addedBlock, previousBlock.position);
            }
        }
        else
        {
            score += placementScore * multiplier.AddMultiplier(Block.Perfect);
        }

        previousBlock = addedBlock;
    }

    private void SnapBlock(Transform block, Vector3 position)
    {
        block.transform.position = new Vector3(position.x, block.position.y, position.z);
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
                    PopupController.CreateFloatingText("Perfect", blue);
                    AudioSystem.Instance.PlayStackSound(true);
                    break;
                case Block.Good:
                    multiplier += Good;
                    PopupController.CreateFloatingText("Good", green);
                    AudioSystem.Instance.PlayStackSound(true);
                    break;
                case Block.Ok:
                    multiplier += Ok;
                    PopupController.CreateFloatingText("Ok", yellow);
                    AudioSystem.Instance.PlayStackSound(false);
                    break;
                case Block.Bad:
                    multiplier = 1;
                    AudioSystem.Instance.PlayStackSound(false);
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
