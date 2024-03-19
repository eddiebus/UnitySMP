using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



[ExecuteAlways]
public class CanvasSwitcher : MonoBehaviour
{
    // If you really wanted to use fields this is the way
    [field: SerializeField]
    public int ActiveIndex { get; private set; } = -1;
    public Action OnCanvasSwitch = ()=>{};

    void Start(){
        OnCanvasSwitch.Invoke();
    }
    
    private void Update()
    {
        _Refresh();
        _EditorRefresh();
    }

    /*
    Editor Only:
    Hide Child Switcher elements that are not currently selected in editor
    */
    private void _EditorRefresh()
    {
        if (Application.isPlaying) return;
        if (Selection.activeGameObject)
        {

            if (Selection.activeGameObject.transform.IsChildOf(this.transform))
            {
                // Itterate switcher's children
                for (int i = 0; i < transform.childCount; i++)
                {
                    // Get Children
                    var childTransform = transform.GetChild(i);
                    bool selected = Selection.activeGameObject == childTransform.gameObject
                    ||  Selection.activeGameObject.transform.IsChildOf(childTransform);
                    childTransform.gameObject.SetActive(selected);
                }
                return;
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == ActiveIndex);
        }

    }

    private void _Refresh()
    {
        if (Application.isPlaying)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var childObj = transform.GetChild(i).gameObject;
                transform.GetChild(i).gameObject.SetActive(i == ActiveIndex);
            }
        }
    }

    public void SetActiveIndex(int newIndex)
    {
        ActiveIndex = newIndex;
        OnCanvasSwitch.Invoke();
    }

}
