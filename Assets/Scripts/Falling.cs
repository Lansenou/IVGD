using UnityEngine;

public class Falling : MonoBehaviour
{
    public float MaxDist = 1.5f;

    private float startY;

    // Use this for initialization
    private void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!FallManager.DidFall && startY - transform.position.y > MaxDist)
        {
            FallManager.DidFall = true;
        }
    }
}