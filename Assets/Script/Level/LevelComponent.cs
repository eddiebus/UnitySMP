using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level
{
    protected static Level _Instance;

    public static Action OnLevelStart = new Action(() => { });
    public static Action OnLevelEnd = new Action(() => { });
    public static Action OnLevelQuit = new Action(() => { });

    public static Action OnBossStart = new Action(() => { });
    public static Level Get()
    {
        if (_Instance == null)
        {
            _Instance = new Level();
        }
        return _Instance;
    }
}

public class LevelComponent : MonoBehaviour
{
    public int LevelNumber = 0;  
    public string LevelName = "SomeWhere";
    // Awake. Called soon as gameobject is activated
    void Awake()
    {
        Level.OnLevelStart.Invoke();
    }

}
