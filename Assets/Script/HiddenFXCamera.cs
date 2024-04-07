using System.Linq;
using UnityEngine;



[ExecuteAlways]
public class HiddenFXCamera : MonoBehaviour
{

    public static HiddenFXCamera[] AllInstance => GetAllCamera();

    public float OrthoSize;
    public Camera _camComponent;
    public RenderTexture _renderTexture;

    // Start is called before the first frame update
    void Start()
    {
        _FXCameraInit();
    }

    // Update is called once per frame
    void Update()
    {
        _UpdateCamera();
    }

    protected void _FXCameraInit()
    {
        _camComponent = GetComponent<Camera>();
        _renderTexture = new RenderTexture(640, 640, 32);
        _renderTexture.name = "HiddenFX Render Texture";

        if (_camComponent)
        {
            var halfSize = OrthoSize / 2;
            _camComponent.projectionMatrix = Matrix4x4.Ortho(
                -halfSize, halfSize,
                -halfSize, halfSize,
                0.1f, 1000
            );
        }
    }

    private static HiddenFXCamera[] GetAllCamera()
    {
        return FindObjectsByType<HiddenFXCamera>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.InstanceID
            ).ToArray();
    }

    public RenderTexture GetTexture()
    {
        return _renderTexture;
    }

    public void _UpdateCamera()
    {
        if (!_camComponent) return;

        if (_camComponent.orthographic)
        {
            float orthoHalf = OrthoSize / 2;
            _camComponent.depth = -1000;

            var halfSize = OrthoSize / 2;
            _camComponent.projectionMatrix = Matrix4x4.Ortho(
                -halfSize, halfSize,
                -halfSize, halfSize,
                0.1f, 1000
            );
            _camComponent.targetTexture = _renderTexture;
            _camComponent.Render();
        }
    }
}
