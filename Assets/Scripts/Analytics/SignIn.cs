using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Assets.Scripts.Utility;
using JetBrains.Annotations;

public class SignIn : Singleton<SignIn>
{
    private int goodStacks = 0;
    private int perfectStacks = 0;
    private int destroyedObjects = 0;

    void Start()
    {
 
        // Create client config
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();


        // Sign in the user
        Social.localUser.Authenticate((bool succes) =>
        {
            if (succes)
            {
                // handle succes or failure. TODO: show standby screen?
                // complete welcome achievement.
                WelcomeAchievement(100f);
            }
            else
            {
                Debug.LogError("Failed to authenticate");
            }
        });
    }

    //Use completionValue to complete or increment the achievement.
    private void WelcomeAchievement(float completionValue)
    {
        PlayGamesPlatform.Instance.ReportProgress(
            TitsResources.achievement_welcome_to_stack__destroy,
            completionValue, (bool succes) =>
            {
                Debug.Log("Welcome unlock: " + succes);
            });
    }

    public void StackerAchievement(float completionValue)
    {
        int achievementScore = 20;

        if (HighScore.instance.CurrentScore >= achievementScore)
        {
            PlayGamesPlatform.Instance.ReportProgress(
                TitsResources.achievement_stacker, completionValue, (bool succes) =>
                {
                    Debug.Log("Stacker unlock: " + succes);
                });
        }
    }

    public void IncreaseGoodStacks(int value)
    {
        goodStacks += value;
    }

    public void IncreasePerfectStacks(int value)
    {
        perfectStacks += value;
    }

    public void GoodStackerAchievement(float completionValue)
    {
        int achievementScore = 10;
        if (goodStacks >= achievementScore)
        {
            PlayGamesPlatform.Instance.ReportProgress(
                TitsResources.achievement_perfect_stacker, completionValue, (bool succes) =>
                {
                    Debug.Log("Good stacker unlock: " + succes);
                });
        }

    }

    public void PerfectStackerAchievement(float completionValue)
    {
        int achievementScore = 10;
        if (goodStacks >= achievementScore)
        {
            PlayGamesPlatform.Instance.ReportProgress(
                TitsResources.achievement_perfect_stacker, completionValue, (bool succes) =>
                {
                    Debug.Log("Good stacker unlock: " + succes);
                });
        }
    }

    public void IncreaseDestroyedObjects(int value)
    {
        destroyedObjects += value;
    }

    public void DestroyerAchievement(float completionValue)
    {
        int achievementScore = 10;

        if(destroyedObjects >= achievementScore) { 
            PlayGamesPlatform.Instance.ReportProgress(
                TitsResources.achievement_destroyer, completionValue, (bool succes) =>
                {
                    Debug.Log("Destroyer unlock: " + succes);
                });
        }
    }

    public void TheCarGuyAchievement(float completionValue)
    {
        PlayGamesPlatform.Instance.ReportProgress(
            TitsResources.achievement_the_car_guy, completionValue, (bool succes) =>
            {
                Debug.Log("The car guy unlock: " + succes);
            });
    }

    public void SlackerAchievement(float completionValue)
    {
        PlayGamesPlatform.Instance.ReportProgress(
            TitsResources.achievement_destroyer, completionValue, (bool succes) =>
            {
                Debug.Log("Slacker unlock: " + succes);
            });
    }
}
