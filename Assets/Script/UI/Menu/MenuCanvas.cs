using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public enum MenuCanvasState{
    Opened,
    Closed
}

public class MenuCanvas : MonoBehaviour
{
    protected MenuCanvasState MenuState = MenuCanvasState.Closed;
    public bool OpenOnStart = false;
    public GameObject DefaultObject = null;
    UnityEvent OnMenuClose;
    UnityEvent OnMenuOpen;
    void Start()
    {

        if (OpenOnStart && DefaultObject){
            EventSystem.current.SetSelectedGameObject(DefaultObject);
            MenuState = MenuCanvasState.Opened;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
