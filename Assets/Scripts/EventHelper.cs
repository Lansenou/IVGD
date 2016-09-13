using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHelper : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}