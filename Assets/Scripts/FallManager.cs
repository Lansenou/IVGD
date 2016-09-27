using System.Collections;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    public static bool DidFall;
    public int WaitForSeconds = 5;

    private bool hasFallen;
    [SerializeField] private GameObject target;

    private void Awake()
    {
        DidFall = false;
    }

    private void Start()
    {
        DidFall = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (hasFallen != DidFall)
        {
            StartCoroutine(EnableTarget(hasFallen = DidFall));
        }
    }

    private IEnumerator EnableTarget(bool active) {
        yield return new WaitForSeconds(WaitForSeconds);
        target.SetActive(active);
    }
}