using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    private GameObject parent;
    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.Sleep();
    }
    
	void Update () {
	    if (parent != null)
	    {
	        this.transform.position = parent.transform.position;
	    }
	}

    public void Attach(GameObject parent)
    {
        this.parent = parent;
    }

    public void Release()
    {
        this.parent = null;
        rigidBody.WakeUp();
    }
}
