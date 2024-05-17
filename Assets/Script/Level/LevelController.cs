using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LevelController : MonoBehaviour
{
    public UnityEvent OnLevelStart = new UnityEvent();
    public UnityEvent OnLevelEnd = new UnityEvent();
    public UnityEvent OnLevelQuit = new UnityEvent();

    public UnityEvent OnBossStart = new UnityEvent();
    void Awake()
    {
        //Subscribe to Level Events
        Level.OnLevelStart += this.OnLevelStart.Invoke;
        Level.OnLevelEnd += this.OnLevelEnd.Invoke;
        Level.OnLevelQuit += this.OnLevelQuit.Invoke;

        Level.OnBossStart += this.OnBossStart.Invoke;
    }

    // GameObject Destructor
    private void OnDestroy()
    {
        // Remove event/delegate links
        Level.OnLevelStart -= this.OnLevelStart.Invoke;
        Level.OnLevelEnd -= this.OnLevelEnd.Invoke;
        Level.OnLevelQuit -= this.OnLevelQuit.Invoke;

        Level.OnBossStart -= this.OnBossStart.Invoke;
    }

    public void StartLevel() => Level.OnLevelStart.Invoke();
    public void EndLevel() => Level.OnLevelEnd.Invoke();
    public void QuitLevel() => Level.OnLevelQuit.Invoke();

}
