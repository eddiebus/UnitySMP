using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




[ExecuteAlways]
public class GameCamera : MonoBehaviour
{
    [Range(30,110)]
    public float fov;
    [Range(10,1000)]
    public float zFar;
    public static Vector2 TargetRatio = new Vector2(9,16);
    public Camera _CamComponent;
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

    private void _UpdateCamera(){
        if (!_CamComponent) return;
        _CamComponent.orthographic = false;

        var ProjMatrix = Matrix4x4.Perspective(
            fov,
            (float)Screen.width/ Screen.height,
            0.01f,zFar
        );

        _CamComponent.farClipPlane = zFar * 1.1f;

        _CamComponent.projectionMatrix = ProjMatrix;
        _CamComponent.transform.rotation = Quaternion.LookRotation(Vector3.forward);

        var HeightHalf = TargetRatio.y / 2;
        var TargetDistance = HeightHalf /  Mathf.Tan( (Mathf.PI/180.0f) * fov/2) ;

        Vector3 camPosition = new Vector3 (
            0,0,
            -TargetDistance
        );

        transform.position = camPosition;
    }


    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        float depth = 10.0f;
        Gizmos.DrawWireCube(
            Vector3.forward * (depth/2),
            new Vector3(9,TargetRatio.y,depth)
        );
    }
}
