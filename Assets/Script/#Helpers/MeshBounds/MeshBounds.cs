using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBounds
{
    public static Bounds Get(GameObject obj){
        var renderers = obj.GetComponentsInChildren<Renderer>();
        List<Bounds> allBounds = new List<Bounds>();

        foreach (var render in renderers){
            allBounds.Add(render.bounds);
        }

        if (allBounds.Count > 0){
            Bounds totalBound = allBounds[0];
            for (int i = 1; i < allBounds.Count ; i++){
                totalBound.Encapsulate(allBounds[i]);
            }
            return totalBound;
        }
        else{
            // No Renderers under object return empty bounds
            return new Bounds(obj.transform.position, Vector3.zero);
        }
    }
}
