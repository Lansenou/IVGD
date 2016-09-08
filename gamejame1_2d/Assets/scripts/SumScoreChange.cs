using UnityEngine;
using System.Collections;

public class SumScoreChange : MonoBehaviour
{

    bool timed = false;
    public void AddPoints(int points)
    {
        SumScore.Add(points);
    }

    public void SubtractPoints(int points)
    {
        SumScore.Add(-points);
        // NOTE - You can also use SumScore.Subtract(points) if you like typing
    }

    public void ToggleTimed()
    {
        timed = !timed;
    }

    public void ResetPoints()
    {
        SumScore.Reset();
    }

    public void CheckHighScore()
    {
        if (SumScore.Score > SumScore.HighScore)
            SumScore.SaveHighScore();
    }

    public void ClearHighScore()
    {
        SumScore.ClearHighScore();
    }

    void Update()
    {
        if (timed)
            SumScore.Add(Mathf.RoundToInt(Time.deltaTime * 100));
    }
}


