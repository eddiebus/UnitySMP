using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;



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

    public static GameManager Get()
    {
        if (_Instance == null)
        {
            _Instance = new GameManager();
        }
        return _Instance;
    }
}


