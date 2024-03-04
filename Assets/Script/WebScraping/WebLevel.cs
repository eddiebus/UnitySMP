using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class WebLevel : MonoBehaviour
{

    public string[] URLSet;
    public Material RenderMat;
    public Material CompMaterial;
    public RenderTexture CompTexture;
    public List<Texture2D> collection;

    public bool completed = false;
    public int ScannedTexture = 0;
    public int ScannedSites = 0; 


    public bool TextureBusy;
    // Start is called before the first frame update
    void Start()
    {
        CompTexture = new RenderTexture(2046, 2046, 8, RenderTextureFormat.Default);
        _init().ContinueWith(result =>
        {
            completed = true;
        });

        RenderMat = new Material(
            Shader.Find("Unlit/Texture")
        );

        CompMaterial = new Material(
            Shader.Find("Unlit/Texture")
        );
        CompMaterial.SetTexture("_MainTex", CompTexture);

    }
    
    async private Task _init()
    {
        foreach (var url in URLSet)
        {
            var imgLinks = WebScraper.GetAllImageLinksFromPage(url);

            foreach (var link in imgLinks)
            {
                await WebScraper.DownloadWebImage(link).ContinueWith(
                    tex => {
                        if (tex.Result != null){
                            collection.Add(tex.Result);
                        }
                        ScannedTexture++;
                    }
                );
            }
            ScannedTexture++; 

        }
        completed = true;
        return;
    }

    // Get Instance in Scene
    public static WebLevel Get()
    {
        return FindFirstObjectByType<WebLevel>();
    }

    // Texture Get Function
    public Texture2D GetTexture(int Index){
        if (collection.Count == 0) return null;
        else {
            return  collection[Index];
        }
    }

    public Texture2D GetRandomTexture(){
        if (collection.Count == 0) return null;
        
        var numGen = new System.Random();
        var randomIndex = (int) (numGen.NextDouble() * (collection.Count-1));
        return collection[randomIndex];
    }

}
