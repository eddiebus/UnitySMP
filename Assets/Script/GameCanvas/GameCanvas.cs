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
        if (_canvas)
        {
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _canvas.planeDistance = 15.0f; 
            _canvas.worldCamera = GameCamera.Get().CamComponent;
        }
    }
}
