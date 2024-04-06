using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;



public class MenuCanvas : MonoBehaviour
{
    public bool IsOpen = false;
    public GameObject DefaultObject = null;
    public UnityEvent OnMenuOpen;
    public UnityEvent OnMenuClose;

    public Animator _AnimatorComponent = null;
    protected static string AnimComp_MenuOpen = "MenuOpen";
    void Awake()
    {
        _AnimatorComponent = GetComponent<Animator>();
        if (IsOpen)
        {
            SetFocusWidget(DefaultObject);
        }

        // Check for CanvasSwitcher as Parent
        if (transform.parent)
        {
            var switcher = transform.parent.gameObject.GetComponent<CanvasSwitcher>();
            if (switcher != null)
            {
                switcher.OnCanvasSwitch += () =>
                {
                    if (GetIndexInParent() == switcher.ActiveIndex){
                        this.IsOpen = true;
                        OnMenuOpen.Invoke();
                        SetFocusWidget(DefaultObject);
                    }
                    else{
                        this.IsOpen = false;
                    }
                };
            }
            else{
                this.IsOpen = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _UpdateAnimator();
    }

    protected void _UpdateAnimator()
    {
        if (Application.isPlaying && _AnimatorComponent)
        {
            _AnimatorComponent.SetBool(
                AnimComp_MenuOpen,
                IsOpen
            );
        }
    }

    // Set Obj to focus on menu open
    public void SetDefaultObj(GameObject targetObj)
    {
        DefaultObject = targetObj;
    }

    // Set Focus Widget
    protected void SetFocusWidget(GameObject targetObj)
    {
        if (Application.isPlaying && targetObj)
        {
            EventSystem.current.SetSelectedGameObject((GameObject)targetObj);
        }
    }

    // Get what Index this transform is in parent
    private int GetIndexInParent()
    {
        if (transform.parent)
        {
            var parent = transform.parent;
            for (int i = 0; i < parent.childCount; i++)
            {
                if (parent.GetChild(i) == this.transform)
                {
                    return i;
                }
            }
        }
        return -1;
    }


    public void CloseMenu()
    {
        IsOpen = false;
        OnMenuClose.Invoke();
    }

}
