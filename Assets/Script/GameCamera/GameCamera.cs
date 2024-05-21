using UnityEngine;
using UnityEngine.UIElements;




[ExecuteAlways]
public class GameCamera : MonoBehaviour
{
    [Range(30, 110)]
    public float fov;
    [Range(10, 1000)]
    public float zFar;
    public static Vector2 TargetRatio = new Vector2(9, 20);
    public Camera _CamComponent;
    public Camera CamComponent => _CamComponent;
    // Start is called before the first frame update
    void Start()
    {
        _CamComponent = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _UpdateCamera();
    }

    public static GameCamera Get() => FindFirstObjectByType<GameCamera>();


    // Update projection matrix
    // Bse on FOV and target play area
    private void _UpdateCamera()
    {


        // Vertical Check
        // check if screen in thinner than target ratio
        float targetAspectFloat = TargetRatio.x / TargetRatio.y;
        float currentAspectRatio = (float)Screen.width / Screen.height;
        float ratioDifference = Mathf.Abs(currentAspectRatio - targetAspectFloat) / targetAspectFloat;

        float camAspecRatio = currentAspectRatio;
        // Letterbox Viewport
        if (currentAspectRatio < targetAspectFloat)
        {
            float vertMargin = ratioDifference / 2.0f;
            _CamComponent.rect = new Rect(
                0.0f,
                0.0f  + vertMargin,
                1.0f,
                1.0f - (vertMargin * 2.0f)
                );

            camAspecRatio = targetAspectFloat;
        }
        // Default Viewport
        else
        {
            _CamComponent.rect = new Rect(
                0.0f,
                0.0f,
                1.0f,
                1.0f
                );
        }


        if (!_CamComponent) return;
        _CamComponent.orthographic = false;

        var ProjMatrix = Matrix4x4.Perspective(
            fov,
            camAspecRatio,
            0.01f, 10000
        );

        _CamComponent.farClipPlane = zFar * 1.1f;
        _CamComponent.projectionMatrix = ProjMatrix;
        _CamComponent.transform.rotation = Quaternion.LookRotation(Vector3.forward);

        // Get desired distance from play area to view
        var HeightHalf = TargetRatio.y / 2;
        var TargetDistance = HeightHalf / Mathf.Tan((Mathf.PI / 180.0f) * fov / 2);

        Vector3 camPosition = new Vector3(
            0, 0,
            -TargetDistance
        );

        transform.position = camPosition;
    }

    public Bounds GetBounds() => new Bounds(
        Vector3.zero,
        new Vector3(TargetRatio.x, TargetRatio.y, 1000)
    );

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float depth = 10.0f;
        Gizmos.DrawWireCube(
            Vector3.forward * (depth / 2),
            new Vector3(TargetRatio.x, TargetRatio.y, depth)
        );
    }
}
