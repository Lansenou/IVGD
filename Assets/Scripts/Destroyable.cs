using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public int Score;

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("towerBlock"))
        {
            HighScore.CurrentScore += Score;
            Destroy(gameObject);
        }
    }
}