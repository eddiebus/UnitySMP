using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class TerrainCamera : MonoBehaviour
{
    public Camera _camComponent;
    public RenderTexture Texture;
    // Start is called before the first frame update
    void Start()
    {
        _camComponent = GetComponent<Camera>();
        Texture = new RenderTexture(640, 640, 32);
        Texture.name = "Render Texture";
    }

    // Update is called once per frame
    void Update()
    {
        _UpdateCamera();

    }

    private void _UpdateCamera()
    {
        if (!_camComponent) return;
        _camComponent.targetTexture = Texture;
    }

    public static TerrainCamera[] GetAllCamera()
    {
        return FindObjectsByType<TerrainCamera>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID).ToArray();
    }

}
