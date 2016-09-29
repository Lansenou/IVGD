using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingPopupText : MonoBehaviour {

    public Animator animator;
    private Text text; 
    // Use this for initialization
    void Start ()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        text = animator.GetComponent<Text>();
    }

    public void setText(string String)
    {
        animator.GetComponent<Text>().text = String;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
