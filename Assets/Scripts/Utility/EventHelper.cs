using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Utility
{
    public class EventHelper : MonoBehaviour
    {
        public void RestartScene()
        {
            ResetManager.Instance.Reset();
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
        public void ResetScene()
        {
            foreach (var findObject in InterfaceHelper.FindObjects<IResettable>())
            {
                findObject.Reset();
            }
        }
    }

}