using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputSystemEventHandler
{
    public static InputSystemEventHandler _Instance = null;

    public Action OnPointerDown = new Action (() => { });
    public static InputSystemEventHandler GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new InputSystemEventHandler();
        }
        return _Instance;
    }
}
public class InputSystemEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
