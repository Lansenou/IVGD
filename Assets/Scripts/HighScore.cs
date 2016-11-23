using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public static float CurrentScore;

    private float highScore = 0;
    private float prevScore = 0;

    [SerializeField] private Text text;
    [SerializeField] private Text text2;

    // Use this for initialization
    private void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        text2.text = "HighScore: " + highScore.ToString("0");   
        text.text = "" + (CurrentScore = 0);
    }

    private void Update()
    {
        if (CurrentScore > prevScore)
        {
            text.text = "" + (prevScore = CurrentScore).ToString("0");
            if (CurrentScore > highScore)
            {
                text2.text = "HighScore: " + (highScore = CurrentScore).ToString("0");
                PlayerPrefs.SetFloat("HighScore", CurrentScore);
            }
            Social.ReportScore(System.Convert.ToInt64(highScore), TitsResources.leaderboard_stack_score, (bool success) => { });
        }
    }
}