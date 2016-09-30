using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public static int CurrentScore;

    private int highScore = 0;
    private int prevScore = 0;

    [SerializeField]
    private Text text;
    [SerializeField]
    private Text text2;

    // Use this for initialization
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        text2.text = "HighScore: " + highScore;
        text.text = "" + (CurrentScore = 0);
    }

    private void Update()
    {
        if (CurrentScore > prevScore)
        {
            text.text = "" + (prevScore = CurrentScore);
            if (CurrentScore > highScore)
            {
                text2.text = "HighScore: " + (highScore = CurrentScore);
                PlayerPrefs.SetInt("HighScore", CurrentScore);
            }
        }
    }
}