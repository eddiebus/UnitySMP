using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WebSpaceImage : MonoBehaviour
{
    public HiddenFXCamera _ParentCamera;
    private float _InitLifeTime;
    public float LifeTime;

    public Quaternion MoveRotation;

    public Texture2D MyTexture;
    public Material _RenderMat;
    public Mesh myMesh;
    // Start is called before the first frame update
    void Start()
    {
        _Init();
    }

    private Mesh _GenerateMesh()
    {
        Mesh returnMesh = new Mesh();
        returnMesh.name = "Plane";

        float width = 0.5f;
        returnMesh.vertices = new Vector3[] {
            new Vector3 (-width,0,-width), //Bottom Left
            new Vector3 (-width,0,width), // Top Left
            new Vector3 (width,0,width), //Top Right
            new Vector3 (width,0,-width) //Bottom right 
        };


        returnMesh.triangles = new int[]{
            0,1,2,
            0,2,3
        };

        returnMesh.uv = new Vector2[] {
            Vector2.zero,
            Vector2.up,
            Vector2.up + Vector2.right,
            Vector2.right
        };
        returnMesh.RecalculateNormals();

        return returnMesh;
    }

    private void _Init()
    {

        float lifeRange = 5.0f;
        _InitLifeTime = (float)(new System.Random().NextDouble()) * lifeRange;
        LifeTime = _InitLifeTime;
        _RenderMat = new Material(
            Shader.Find("Standard")
        );

        myMesh = _GenerateMesh();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = myMesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = _RenderMat;
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        
        MoveRotation = Quaternion.Euler(0,UnityEngine.Random.Range(0,360),0);
        _MoveToNewPosition();
    }

    private void _MoveToNewPosition(){
        if (!_ParentCamera) return;

        this.transform.position = 
        _ParentCamera.transform.position +
        _ParentCamera.transform.forward;

        float ortho = _ParentCamera.OrthoSize;
        var numGen = new System.Random();
        Vector3 Offset = new Vector3(
            ((float)numGen.NextDouble() * ortho) - ortho /2,
            0,
            ((float)numGen.NextDouble() * ortho) - ortho /2 
        );
        this.transform.position += Offset;
    }
    
    private void _Slide(){
        this.transform.position += MoveRotation * Vector3.forward  * Time.deltaTime;
    }
    private void _UpdateMaterial()
    {
        float LifeTimeFloat = LifeTime / (_InitLifeTime * 0.25f);
        _RenderMat.SetColor(
            "_Color",
            new Color(1, 1, 1, LifeTimeFloat)
        );
        _RenderMat.SetOverrideTag("RenderType", "Transparent");
        _RenderMat.SetFloat("_Mode", 3);
        _RenderMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _RenderMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _RenderMat.SetInt("_ZWrite", 0);
        _RenderMat.DisableKeyword("_ALPHATEST_ON");
        _RenderMat.EnableKeyword("_ALPHABLEND_ON");
        _RenderMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");

        //_RenderMat.SetTexture("_MainTex", WebLevel.Get().GetRandomTexture());
    }

    private void _Tick()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            _RenderMat.SetTexture("_MainTex", WebLevel.Get().GetRandomTexture());
            LifeTime = _InitLifeTime;
            _MoveToNewPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _Tick();
        _Slide();
        _UpdateMaterial();

    }

    void OnDrawGizmos(){ 
        #if UNITY_EDITOR
        if (Selection.activeObject == this.gameObject){
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position,
            transform.position + (MoveRotation * Vector3.forward) );
        }
        #endif
    }
}
