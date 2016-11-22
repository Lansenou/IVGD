using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHelper : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void OpenAchievements()
    {
        // show achievements UI
        if(!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool succes) =>
            {
                // handle succes or failure. 
                if (!succes)
                {
                    // Failed
                    return;
                }
                else
                {
                    Social.ShowAchievementsUI();
                }
            });
        }
        else
        {
            Social.ShowAchievementsUI();
        }
    }
}