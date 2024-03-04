using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebTerrain : MonoBehaviour
{
    public Terrain _TerrainComp;
    public Material RenderMat;
    // Start is called before the first frame update
    void Start()
    {
        _TerrainComp = GetComponent<Terrain>();
        RenderMat = new Material(
            Shader.Find("Unlit/Texture")
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (HiddenFXCamera.AllInstance != null)
        {
            _TerrainComp.materialTemplate = RenderMat;
            RenderMat.SetTexture("_MainTex", HiddenFXCamera.AllInstance[0].GetTexture());
        }
    }
}
