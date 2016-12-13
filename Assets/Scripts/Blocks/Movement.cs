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

    public GameObject TimerObject;

    [SerializeField]
    private float movementSpeed = 1f;
    private float distanceCheck = 0.001f;

    // Use this for initialization
    private void Start()
    {
        // 1 Higher than the base
        transform.position = basePosition = Base.position + new Vector3(0, 2.75f);

        // Base position is the middle of the pattern, from which the block moves.
        currentCycle = Pattern.GetRandomCycle();
        gameObject.GetComponent<BlockSpawner>().SetCurrentColor(currentCycle.Color);
        TimerObject.GetComponent<TimerScript>().SetTime(currentCycle.GetTime());
    }

    void Update()
    {
        if (!FallManager.DidFall && PauseMenu.CurrentStatus == PauseMenu.Status.Inactive)
        {
            MoveBlock();
        }
    }

    void MoveBlock()
    {
        targetPosition = basePosition + currentCycle.GetCurrentTarget().TargetPos;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(targetPosition, transform.position) < distanceCheck)
        {
            currentCycle.IncreaseIndex();
        }
    }

    public void MoveUp() {
        StartCoroutine(MoveY());
        currentCycle = Pattern.GetRandomCycle();
        basePosition.y += AddHeight;

        // Set next position
        Vector3 newPosition = basePosition + currentCycle.GetCurrentTarget().TargetPos;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        currentCycle.IncreaseIndex();


        gameObject.GetComponent<BlockSpawner>().SetCurrentColor(currentCycle.Color);
        TimerObject.GetComponent<TimerScript>().SetTime(currentCycle.GetTime());
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
            transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
            yield return null;
        }
        startPosition.y = targetPosition.y = targetY;
    }
}
