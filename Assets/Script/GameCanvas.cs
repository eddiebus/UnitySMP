using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GameCanvas : MonoBehaviour
{
    public Canvas _canvas;
    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canvas){
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = GameCamera.Get().CamComponent;
        }
    }
}
