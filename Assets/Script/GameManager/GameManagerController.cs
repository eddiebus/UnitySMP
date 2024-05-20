using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameManagerController : MonoBehaviour
{
    public UnityEvent OnGamePause;
    public UnityEvent OnGameResume;

    private void Awake()
    {
        GameManager.Get().OnGamePause += this.OnGamePause.Invoke;
        GameManager.Get().OnGameResume += this.OnGameResume.Invoke;

        OnGamePause.AddListener(() =>
        {
            Debug.Log($"GameManager : Game Paused");
        });
    }

    public void TogglePause(bool pause)
    {
        GameManager.Get().Pause = pause;
    }


    public void SayHello()
    {
    }
}