using System;
using UnityEngine;

public class AI : MonoBehaviour
{
    [Range(0.1f, 1.0f)]
    public float ThinkTime = 0.5f;
    private float _TimeTillThink;

    protected void AIInit()
    {
        _TimeTillThink = ThinkTime;
    }

    protected void AITick()
    {

        if (_TimeTillThink > 0)
        {
            _TimeTillThink -= Time.deltaTime;
        }
        else
        {
            HandleAICycle();
            _TimeTillThink = ThinkTime;
        }

    }


    protected virtual void HandleAICycle()
    {
        throw new NotImplementedException("AI Cycle State Not Implimented on AI Class");
    }

}
