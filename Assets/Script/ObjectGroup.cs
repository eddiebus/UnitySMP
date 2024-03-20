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
        if (GetComponentInChildren<Enemy>() == null && Application.isPlaying)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        #if UNITY_EDITOR
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
        #endif
    }
}
