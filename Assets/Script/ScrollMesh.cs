using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.SceneManagement;
using UnityEngine;


/*
Scroll Mesh:
Endlessly scroll mesh object withing bounds of parent.
*/
[ExecuteAlways]
public class ScrollMesh : MonoBehaviour
{
    public Vector3 ScrollVector = Vector3.down;
    protected bool originalObj = true; // Was this the 1st Obj (Not created by script)
    public GameObject up;
    public GameObject down;

    // Update is called once per frame
    void Update()
    {
        Scroll();
        CheckSiblings();
        EditRefresh();
    }

    public void Scroll()
    {
        if (Application.isPlaying)
        {
            transform.position += ScrollVector * Time.deltaTime;
        }
    }


    protected static bool _CanEditPreview()
    {
        if (Application.isPlaying == false
        && Selection.activeGameObject != null
        && PrefabStageUtility.GetCurrentPrefabStage() == null)
        {
            var selectObj = Selection.activeObject;
            if (selectObj.GetComponent<ScrollMesh>())
            {
                return true;
            }
        }


        return false;
    }


    protected void CheckSiblings()
    {
        // Preview when selection scroll mesh
        // Check if scroll mesh is selected
        if (
            !Application.isPlaying && Selection.activeGameObject != null
        )
        {
            if (
                Selection.activeGameObject.GetComponent<ScrollMesh>() == null
            )
            {
                return;
            }
        }
        Bounds Area = new Bounds(
            transform.parent.position,
            Vector3.one * 10
        );
        Bounds myBounds = MeshBounds.Get(gameObject);

        if (Area.Intersects(myBounds))
        {
            if (up == null)
            {
                GameObject copyObj = GameObject.Instantiate(gameObject);
                copyObj.transform.SetParent(transform.parent);
                copyObj.transform.localPosition = this.transform.localPosition +
                (Vector3.up * myBounds.size.y);
                copyObj.transform.localRotation = transform.localRotation;
                copyObj.GetComponent<ScrollMesh>().originalObj = false;
                copyObj.GetComponent<ScrollMesh>().down = this.gameObject;
                up = copyObj;
            }
            else if (down == null)
            {
                GameObject copyObj = GameObject.Instantiate(gameObject);
                copyObj.transform.SetParent(transform.parent);
                copyObj.transform.localPosition = this.transform.localPosition +
                (Vector3.down * myBounds.size.y);
                copyObj.transform.localRotation = transform.localRotation;
                copyObj.GetComponent<ScrollMesh>().originalObj = false;
                copyObj.GetComponent<ScrollMesh>().up = this.gameObject;
                down = copyObj;
            }
        }
        else
        {
            Bounds paddedArea = Area;
            paddedArea.Expand(
                myBounds.size.magnitude * 2.0f
            );
            if (!paddedArea.Intersects(myBounds))
            {
                //Dont Destroy original obj in edit mode
                // Obj created in editor are "original"
                if (Application.isPlaying || !originalObj)
                {
                    DestroySelf();
                }
            }
        }
    }

    void DestroySelf()
    {
        if (Application.isPlaying)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            GameObject.DestroyImmediate(this.gameObject);
        }
    }

    // Refresh Scroll Mesh objects in Edit mode
    public static void EditRefresh()
    {
        if (!_CanEditPreview())
        {
            Debug.Log("Should clear");

            // Delete non-original object
            foreach (var scrollMesh in FindObjectsByType<ScrollMesh>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.InstanceID
                ))
            {
                // Delete Non Orignal Objects
                if (!scrollMesh.originalObj)
                {
                    GameObject.DestroyImmediate(scrollMesh.gameObject);
                }
                else
                {
                    scrollMesh.up = null;
                    scrollMesh.down = null;
                }
            }
        }
    }



    void OnDrawGizmos()
    {
        var meshBounds = MeshBounds.Get(this.gameObject);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(meshBounds.center, meshBounds.size);
    }
}
