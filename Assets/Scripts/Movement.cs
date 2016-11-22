using System.Collections;
using Assets.Scripts.Blocks;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float AddHeight = 1;
    public float HeightSpeed = 0.25f;

    public Pattern Pattern;

    public Transform Base;
    private Vector3 basePosition;

    private Cycle currentCycle;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private Vector3 baseDifference;
    private float movementSpeed = 1f;



    // Use this for initialization
    private void Start()
    {
        // 1 Higher than the base
        transform.position = basePosition = Base.position + Vector3.up * 3f;
        // Base position is the middle of the pattern, from which the block moves.
        basePosition.y = 0;
        currentCycle = Pattern.GetCycleInfo(Cycle.Direction.First);
        gameObject.GetComponent<BlockSpawner>().SetCurrentColor(currentCycle.Color);
        StartCoroutine(Move());
    }


    public void MoveUp() {
        baseDifference.x = basePosition.x - transform.position.x;
        baseDifference.z = basePosition.z - transform.position.z;

        StartCoroutine(MoveY());
        Pattern.SwitchCycles();
        currentCycle = Pattern.GetCycleInfo(Cycle.Direction.First);
        gameObject.GetComponent<BlockSpawner>().SetCurrentColor(currentCycle.Color);
        //startPosition = basePosition + currentCycle.TargetPos;
        //StartCoroutine(Move());
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

            startPosition = transform.position;
            // Base position + the next target
            targetPosition = basePosition + currentCycle.TargetPos;
            // Add currentHeight to the targetPosition;
            targetPosition += new Vector3(0 - baseDifference.x, transform.position.y, -baseDifference.z);

            float currentTime = 0;
            
            // Loop towards new position
            while (currentTime < 1 && !FallManager.DidFall)
            {
                while (PauseMenu.CurrentStatus != PauseMenu.Status.Inactive)
                {
                    yield return null;
                }
                currentTime += Time.deltaTime/ movementSpeed;
                transform.position = Vector3.Lerp(startPosition, targetPosition, currentTime);
                yield return null;
            }
            //currentCycle.MovementTime = movementSpeed;
            movementSpeed = movementSpeed - 0.0025f;
            currentCycle = Pattern.GetNextCycle(currentCycle);
            
            yield return null;
        }
    }
}