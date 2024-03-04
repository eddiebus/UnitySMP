using System;
using System.Linq;
using HtmlAgilityPack;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class WebScraper
{
    public static string[] GetLinkFromPage(string url)
    {
        List<string> returnList = new List<string>();
        var webFile = new HtmlWeb();
        var doc = webFile.Load(url);
        var imageUrls = doc.DocumentNode.Descendants("img")
        .Select(e => e.GetAttributeValue("src", null))
        .Where(s => !String.IsNullOrEmpty(s));
        return imageUrls.ToArray();
    }

    public static string[] GetAllImageLinksFromPage(string url)
    {
        List<string> returnList = new List<string>();
        var webFile = new HtmlWeb();
        var doc = webFile.Load(url);
        if (doc != null)
        {
            var imageUrls = doc.DocumentNode.Descendants("img")
            .Select(e => e.GetAttributeValue("src", null))
            .Where(s => !String.IsNullOrEmpty(s));
            returnList.AddRange(imageUrls);
        }
        return returnList.ToArray();
    }

    async public static Task<Texture2D> DownloadWebImage(string url)
    {
        UnityWebRequest webTexture = UnityWebRequestTexture.GetTexture(url);

        // Send Web Request
        // Return to this point in code on request completion
        await webTexture.SendWebRequest();

        if (webTexture.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning($"Failed to download image texture from img at : {webTexture.url}");
            return null;
        }
        else
        {
            return ((DownloadHandlerTexture)webTexture.downloadHandler).texture;
        }
    }

    async public static Task<Texture2D[]> GetIMGFromPage(string url)
    {
        List<Texture2D> resultList = new List<Texture2D>();

        List<string> returnList = new List<string>();
        var webFile = new HtmlWeb();
        var doc = webFile.Load(url);
        var imageUrls = doc.DocumentNode.Descendants("img")
        .Select(e => e.GetAttributeValue("src", null))
        .Where(s => !String.IsNullOrEmpty(s));

        foreach (var link in imageUrls)
        {
            UnityWebRequest webTexture = UnityWebRequestTexture.GetTexture(link);

            // Send Web Request
            // Return to this point in code on request completion
            await webTexture.SendWebRequest();

            if (webTexture.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning($"Failed to request image texture from img at : {webTexture.url}");
            }
            else
            {
                Texture2D resultTexture = ((DownloadHandlerTexture)webTexture.downloadHandler).texture;
                if (resultTexture != null)
                {
                    resultList.Add(resultTexture);
                }
            }
        }
        return resultList.ToArray();
    }
}
