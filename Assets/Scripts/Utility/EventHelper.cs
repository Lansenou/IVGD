using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Utility
{
    public class EventHelper : MonoBehaviour
    {
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            TowerPhysics.lastBlock = null;
            OnTap.towerFalling = false;
        }

        public void OpenLeaderboards()
        {
            // show achievements UI
            if (!Social.localUser.authenticated)
            {
                Social.localUser.Authenticate((bool succes) =>
                {
                    // handle succes or failure. 
                    if (!succes)
                    {
                        // Failed
                        Debug.LogError("Failed to authenticate");
                        return;
                    }
                    else
                    {
                        Social.ShowLeaderboardUI();
                    }
                });
            }
            else
            {
                Social.ShowLeaderboardUI();
            }
        }

        public void OpenAchievements()
        {
            // show achievements UI
            if (!Social.localUser.authenticated)
            {
                Social.localUser.Authenticate((bool succes) =>
                {
                    // handle succes or failure. 
                    if (!succes)
                    {
                        // Failed
                        Debug.LogError("Failed to authenticate");
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
}