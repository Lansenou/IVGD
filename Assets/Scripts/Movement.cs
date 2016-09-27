using System.Collections;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class Movement : MonoBehaviour
{
    public float AddHeight = 1;
    public float HeightSpeed = 0.25f;
    public AudioClip PlacementSound;
    public Pattern Pattern;

    private Cycle currentCycle;
    private AudioSource source;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    // Use this for initialization
    private void Start()
    {
        source = GetComponent<AudioSource>();
        currentCycle = Pattern.GetCycleInfo(Cycle.Direction.ForwardX);
        StartCoroutine(Move());
    }

    public void MoveUp() {
        source.PlayOneShot(PlacementSound);
        StartCoroutine(MoveY());
    }

    private IEnumerator MoveY()
    {
        float startY = transform.position.y;
        float targetY = startY + AddHeight;
        float currentTime = 0;

        // Loop towards new height
        while (currentTime < 1 && !FallManager.DidFall)
        {
            currentTime += Time.deltaTime / HeightSpeed;
            // Modify the movement loop variables to make sure the Y position is correctly updated
            startPosition.y = targetPosition.y = Mathf.Lerp(startY, targetY, currentTime);
            yield return null;
        }
        startPosition.y = targetPosition.y = targetY;
    }

    // Update is called once per frame
    private IEnumerator Move()
    {
        while (!FallManager.DidFall)
        {
            while (PauseMenu.CurrentStatus != PauseMenu.Status.Inactive)
            {
                yield return null;
            }

            targetPosition = currentCycle.TargetPos + new Vector3(0, transform.position.y);
            startPosition = transform.position;
            float currentTime = 0;
            
            // Loop towards new position
            while (currentTime < 1 && !FallManager.DidFall)
            {
                while (PauseMenu.CurrentStatus != PauseMenu.Status.Inactive)
                {
                    yield return null;
                }
                currentTime += Time.deltaTime/ currentCycle.MovementTime;
                transform.position = Vector3.Lerp(startPosition, targetPosition, currentTime);
                yield return null;
            }

            currentCycle = Pattern.GetNextCycle(currentCycle);

            yield return null;
        }
    }
}