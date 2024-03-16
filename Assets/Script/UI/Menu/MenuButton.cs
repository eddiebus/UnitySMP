using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    public Animator _AnimatoorComponent;
    const string AnimControl_Focused = "HasFocus";
    // Start is called before the first frame update
    void Start()
    {
        _AnimatoorComponent = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    protected void UpdateAnimatorControl(){

        if (!_AnimatoorComponent) return;
        else {
            _AnimatoorComponent.SetBool(
                AnimControl_Focused,
                EventSystem.current.currentSelectedGameObject == this.gameObject
            );
        }

    }
}
