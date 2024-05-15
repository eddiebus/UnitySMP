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
        Level.Get().OnLevelStart += this.OnLevelStart.Invoke;
        Level.Get().OnLevelEnd += this.OnLevelEnd.Invoke;
        Level.Get().OnLevelQuit += this.OnLevelQuit.Invoke;

        Level.Get().OnBossStart += this.OnBossStart.Invoke;
    }

    // GameObject Destructor
    private void OnDestroy()
    {
        // Remove event/delegate links
        Level.Get().OnLevelStart -= this.OnLevelStart.Invoke;
        Level.Get().OnLevelEnd -= this.OnLevelEnd.Invoke;
        Level.Get().OnLevelQuit -= this.OnLevelQuit.Invoke;

        Level.Get().OnBossStart -= this.OnBossStart.Invoke;
    }

    public void StartLevel() => Level.Get().OnLevelStart.Invoke();
    public void QuitLevel() => Level.Get().OnLevelQuit.Invoke();
}
