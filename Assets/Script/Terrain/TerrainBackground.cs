using UnityEngine;


[ExecuteAlways]
public class TerrainBackground : MonoBehaviour
{
    public MeshRenderer _Renderer;
    public Material RenderMat;

    // Start is called before the first frame update
    void Awake()
    {
        _Renderer = GetComponent<MeshRenderer>();
        RenderMat = new Material(
            //Shader.Find("Unlit/Texture")
            Shader.Find("Standard")
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (!_Renderer) return;
        else
        {
            _Renderer.material = RenderMat;
            var mat = _Renderer.material;
            var TCamera = FindFirstObjectByType<TerrainCamera>();
            mat.SetTexture("_MainTex", TCamera.Texture);
        }
    }
}
