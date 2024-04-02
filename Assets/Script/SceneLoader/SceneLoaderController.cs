using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Range(0.1f, 3.0f)]
    public float MinDelay = 0.5f;
    public static float Progress => GetLoadingProgress();
    public static AsyncOperation loadOp = null;

    private float DelayTime;
    void Start(){
        SceneManager.activeSceneChanged += (Scene active, Scene Next) => {
            OnSceneLoad();
        };
    }

    void Update()
    {
        if (loadOp != null)
        {
            DelayTime += Time.deltaTime;
            if (DelayTime >= MinDelay){
                //loadOp.allowSceneActivation = true;
            }
        }
    }

    private static void OnSceneLoad(){
        loadOp = null;
    }

    public void LoadSceneAsync(string SceneName)
    {
        if (loadOp != null) return;
        loadOp = SceneManager.LoadSceneAsync(SceneName);
        loadOp.allowSceneActivation = false;
    }

    public static void SwitchToNewScene(){
        if (IsLoading()){
            loadOp.allowSceneActivation = true;
        }
    }

    public static AsyncOperation  GetLoadingOperation(){
        return loadOp;
    }

    public static bool IsLoading()
    {
        return loadOp != null;
    }

    public static float GetLoadingProgress()
    {
        if (IsLoading())
        {
            return loadOp.progress;
        }
        else
        {
            return 1.0f;
        }
    }

    
}
