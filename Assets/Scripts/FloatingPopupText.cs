using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingPopupText : MonoBehaviour {

    public Animator animator;
    // Use this for initialization
    void Start ()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
    }

    public void setText(string String)
    {
        animator.GetComponentInChildren<Text>().text = String;
    }

    public void setColor(Color color)
    {
        animator.GetComponentInChildren<Text>().color = color;
    }
}
