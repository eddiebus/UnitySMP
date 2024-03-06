using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
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
    // Start is called before the first frame update
    void Awake()
    {
        Selection.selectionChanged += () =>
        {
            if (Selection.activeGameObject.transform.root.gameObject.GetComponentInChildren<ScrollMesh>() == null)
            {
                ScrollMesh.EditRefresh();
            }
            Debug.Log("Hello Event");
        };
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    public void Scroll()
    {

        if (!Application.isPlaying){
                var selectTransform = Selection.activeGameObject.transform;
                if (!transform.IsChildOf(selectTransform) && selectTransform != transform) return;
        }
        else if (!transform.parent) { return; } // Need a parent to scroll by
        else
        {
            if (Application.isPlaying)
            {
                transform.position += ScrollVector * Time.deltaTime;
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

    public static void EditRefresh()
    {
        foreach (var scrollMesh in FindObjectsByType<ScrollMesh>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.InstanceID
            ))
        {
            if (scrollMesh.originalObj)
            {
                var parentObj = scrollMesh.gameObject.transform.parent.gameObject;
                var siblings = parentObj.GetComponentsInChildren<ScrollMesh>();
                foreach (var component in siblings)
                {
                    if (component != scrollMesh)
                    {
                        GameObject.DestroyImmediate(component.gameObject);
                    }
                }
            }
            else
            {
                continue;
            }
        }
    }
    void _DestroyNonOrignal()
    {
        if (!transform.parent || !originalObj) return;
        else
        {
            var allMesh = transform.parent.gameObject.GetComponentsInChildren<ScrollMesh>();
            foreach (var meshComp in allMesh)
            {
                if (meshComp.originalObj == false)
                {
                    GameObject.Destroy(meshComp.gameObject);
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
