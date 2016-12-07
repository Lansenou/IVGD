using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TowerPhysics : MonoBehaviour
{
    public static float blockCountert = 0;
    public static Transform lastBlock;
    private static Transform towerTransform;

    // Use this for initialization
    void Start ()
    {
        if (towerTransform == null)
            towerTransform = gameObject.transform;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    float d = Vector3.Distance(new Vector3(towerTransform.position.x, 0, towerTransform.position.z), new Vector3(lastBlock.position.x, 0, lastBlock.position.z));
	    if (d > 5f)
	    {
	        OnTap.towerFalling = true;
	    }
	    else
	    {
	        OnTap.towerFalling = false;
	    }
	}

   public void Direction()
   {
       float multiplier = 0.3f+(blockCountert*0.005f);
           if (towerTransform != null && lastBlock != null)
           {
               Vector3 heading = lastBlock.position - towerTransform.position;
               //Debug.Log(heading);
               //Debug.Log(lastBlock.position);
               GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(heading.x*multiplier,0,heading.z*multiplier), lastBlock.position , ForceMode.Impulse);
           }
   }
}
