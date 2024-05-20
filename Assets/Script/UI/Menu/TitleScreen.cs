using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Title Sccreen - Only animate when game is in focus
// For WebGL Compatability reasons
public class TitleScreen : MonoBehaviour
{
    public const string Anim_AppFocus = "AppFocus";
    public Animator animatorComp;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Get();
        animatorComp = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _UpdateAnimator();
    }

    protected void _UpdateAnimator()
    {
        if (!animatorComp) return;
        else
        {
            animatorComp.SetBool(Anim_AppFocus, Application.isFocused);
        }
    }
}
