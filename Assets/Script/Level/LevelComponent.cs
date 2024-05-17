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
    // Start is called before the first frame update
    void Awake()
    {
        Level.OnLevelStart.Invoke();
    }

}
