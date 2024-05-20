using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;



public enum GameMode
{
    Arcade,
    Endless
}

public class GameManager
{
    private static GameManager _Instance;
    public bool Pause
    {
        get { return _Pause; }
        set {
            if (value == _Pause) return;
            else
            {
                _Pause = value;
                switch (value)
                {
                    case true:
                        OnGamePause.Invoke();
                        Time.timeScale = 0;
                        break;
                    case false:
                        OnGameResume.Invoke();
                        Time.timeScale = 1.0f;
                        break;
                }
            }
        }
    }

    protected bool _Pause = false;

    public GameMode gameMode = GameMode.Arcade;

    public System.Action OnGamePause = new System.Action( () => {});
    public System.Action OnGameResume = new System.Action(() => { });

    private GameManager() {
        InputSystem.onEvent += (eventP, device) =>
        {

            bool fullscreen = false;
            if (device is Mouse)
            {
                var currentMouse = device as Mouse;
                if (currentMouse.leftButton.wasPressedThisFrame)
                {
                    Debug.Log($"Gamamaner any click detrected!!!");
                    fullscreen = true;
                }
            }
            else if (device is Touchscreen)
            {
                var ts = device as Touchscreen;
                if (ts.touches[0].phase.ReadValue() 
                    == UnityEngine.InputSystem.TouchPhase.Began)
                {
                    Debug.Log($"Gamamaner any click detrected!!!");
                    fullscreen = true;
                }
            }

            try
            {
                Screen.fullScreen = fullscreen;
            }
            catch
            {
                Debug.LogWarning($"Warning: Request to enter fullscreen failed");
            }

            
        };
    }


    ~GameManager()
    {

    }

    public bool _CheckForAnyPointer()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            return true;
        }
        else
        {
            if (Touchscreen.current != null)
            {
                var primaryTouch = Touchscreen.current.touches[0];
                if (primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
                {
                    return true;
                }
            }
        }
        
        return false;
    }
    

    public static GameManager Get()
    {
        if (_Instance == null)
        {
            _Instance = new GameManager();
        }
        return _Instance;
    }

    

}


