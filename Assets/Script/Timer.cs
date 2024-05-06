using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnComplete;
    public float TimerTime= 1.0f;
    protected float _Time;
    public bool Loop = false;
    public bool DestroyOnComplete = false;
    // Start is called before the first frame update
    void Awake()
    {
        _Time = TimerTime;
    }

    // Update is called once per frame
    void Update()
    {
        _Time -= UnityEngine.Time.deltaTime;
        if (_Time <= 0.0f)
        {
            OnComplete.Invoke();
            if (DestroyOnComplete)
            {
                GameObject.Destroy(this.gameObject);
            }
            else
            {
                if (Loop)
                {
                    _Time = TimerTime;
                }
            }
        }
    }
}
