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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    public void Scroll()
    {
        if (!transform.parent) { return; } // Need a parent to scroll by
        else
        {
            if (Application.isPlaying)
            {
                transform.position += ScrollVector * Time.deltaTime;
            }

            Bounds Area = new Bounds(
                transform.parent.position,
                Vector3.one * 4
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
                    copyObj.GetComponent<ScrollMesh>().originalObj = false;
                    copyObj.GetComponent<ScrollMesh>().down = this.gameObject;
                    up = copyObj;
                }
                else if (down == null){
                    GameObject copyObj = GameObject.Instantiate(gameObject);
                    copyObj.transform.SetParent(transform.parent);
                    copyObj.transform.localPosition = this.transform.localPosition +
                    (Vector3.down * myBounds.size.y);
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

    void OnDrawGizmos()
    {
        var meshBounds = MeshBounds.Get(this.gameObject);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(meshBounds.center, meshBounds.size);
    }
}
