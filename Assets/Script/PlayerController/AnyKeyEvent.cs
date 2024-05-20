using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class AnyKeyEvent : MonoBehaviour
{

    public UnityEvent OnAnyTouch = new UnityEvent();
    // Start is called before the first frame update
    void Awake()
    {
        PlayerController.GetController(0);
    }


    protected void HandleInput()
    {
        var currentMouse = Mouse.current;

        if (currentMouse.leftButton.wasPressedThisFrame)
        {
            this._InvokeEvent();
        }
        else
        {
            var touchScreen = Touchscreen.current;
            if (touchScreen != null)
            {
                if (touchScreen.touches[0].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
                {
                    this._InvokeEvent();
                }
            }
        }
    }

    private void _InvokeEvent()
    {
        if (this.gameObject.activeInHierarchy)
        {
            this.OnAnyTouch.Invoke();
        }
    }


    private void Update()
    {
        HandleInput();
    }

    void OnDestroy()
    {
    }

    

}
