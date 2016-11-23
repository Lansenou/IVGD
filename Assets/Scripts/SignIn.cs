using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Assets.Scripts.Utility;

public class SignIn : Singleton<SignIn>
{
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
                WelcomeAchievement();
            }
            else
            {
                Debug.LogError("Failed to authenticate");
            }
        });
    }

    private void WelcomeAchievement()
    {
        PlayGamesPlatform.Instance.ReportProgress(
            TitsResources.achievement_welcome_to_stack__destroy,
            100.0f, (bool succes) =>
            {
                Debug.Log("Welcome unlock: " + succes);
            });
    }
}
