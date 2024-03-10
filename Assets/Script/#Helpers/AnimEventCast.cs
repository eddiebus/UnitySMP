using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class AnimEventCast : MonoBehaviour
{
    public string EventName;
    public UnityEvent OnInvoke;

    public void AnimEvent(string eventName)
    {
        if (eventName == EventName)
        {
            Debug.Log("$Animation Even Invoked | Event Name = {EventName}");
            OnInvoke.Invoke();
        }
    }
}
