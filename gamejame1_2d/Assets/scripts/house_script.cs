using UnityEngine;
using System.Collections;

public class house_script : MonoBehaviour {

    // Use this for initialization
    void Start () {
        print("script running..");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        print("Detected collision between " + gameObject.name + " and " + col.gameObject.name);
        if (col.gameObject.name == "platform")
        {
            Destroy(gameObject);
            //sound play explosion
            gameObject.GetComponent<SumScoreChange>().AddPoints(10);
        }
    }

}
