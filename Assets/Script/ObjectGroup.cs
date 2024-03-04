using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class ObjectGroup : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0 && Application.isPlaying)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        if (Selection.activeObject == this.gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1.0f);
            foreach (Transform childTransform in transform)
            {
                Gizmos.DrawLine(
                    transform.position,
                    childTransform.position
                ); 
            }
        }
    }
}
