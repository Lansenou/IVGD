using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class Base : MonoBehaviour
{
    public Vector3 Rotation;

    // Update is called once per frame
    private void Update()
    {
        if (FallManager.DidFall)
        {
            Destroy(gameObject);
        }
    }
}