using UnityEngine;
using UnityEngine.UI;

public class FloatingPopupText : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Text text;

    // Use this for initialization
    void Start ()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
    }

    public void SetText(string String)
    {
        text.text = String;
    }

    public void SetColor(Color color)
    {
        text.color = color;
    }
}
