using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AnyKeyEvent : MonoBehaviour
{

    public UnityEvent OnAnyTouch = new UnityEvent();
    // Start is called before the first frame update
    void Awake()
    {
        PlayerController.GetController(0);
        PlayerController.OnAnyClick += this._InvokeEvent;
    }

    private void _InvokeEvent()
    {
        if (this.gameObject.activeInHierarchy)
        {
            this.OnAnyTouch.Invoke();
        }
    }

    void OnDestroy()
    {
        PlayerController.OnAnyClick -= this._InvokeEvent;
    }

    

}
