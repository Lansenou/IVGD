using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public static HighScore instance;

    public static float GetScore
    {
        get { return instance.currentScore; }
    }

    private float currentScore = 0;
    public float CurrentScore
    {
        get { return currentScore; }
        set
        {
            currentScore = value;
            OnScoreUpdate.Invoke(currentScore);
            UpdateScore();
        }
    }

    public UnityAction<float> OnScoreUpdate;
    public UnityAction<float> OnHighScoreUpdate;

    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text highscoreText;

    void Awake()
    {
        instance = this;
        OnHighScoreUpdate += (float score) => { highscoreText.text = "Best " + score.ToString("0"); };
        OnScoreUpdate += (float score) => { scoreText.text = score.ToString("0"); };
        OnHighScoreUpdate.Invoke(highScore = PlayerPrefs.GetFloat("Highscore", 0));
    }

    void OnEnable()
    {
        UpdateScore();
        Reset();
    }

    // Use this for initialization
    void Reset()
    {
        CurrentScore = 0;
    }

    private float highScore = 0;
    private static float prevScore = 0;
    
    public float GetHighscore()
    {
        return highScore;
    }

    public float GetCurrentScore()
    {
        return CurrentScore;
    }

    void UpdateScore()
    {
        if (CurrentScore > prevScore)
        {
            if (CurrentScore > highScore)
            {
                OnHighScoreUpdate.Invoke(highScore = currentScore);
                PlayerPrefs.SetFloat("Highscore", highScore);
               // Debug.Log("Highscore" + highScore);
                prevScore = currentScore;

                if (!Social.localUser.authenticated)
                {
                    Social.localUser.Authenticate((bool succes) =>
                    {
                        if (succes)
                        {
                            Social.ReportScore(System.Convert.ToInt64(highScore), TitsResources.leaderboard_stack_score, (bool success) => { });
                        }
                    });
                }
                else
                {
                    Social.ReportScore(System.Convert.ToInt64(highScore), TitsResources.leaderboard_stack_score, (bool success) => { });
                }
            }
        }
    }
}
