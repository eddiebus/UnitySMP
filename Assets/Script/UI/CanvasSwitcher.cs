using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



[ExecuteAlways]
public class CanvasSwitcher : MonoBehaviour
{
    public int DefaultIndex = 0;
    public  int ActiveIndex => _ActiveIndex;
    private int _ActiveIndex = -1;
    public Action OnCanvasSwitch = () => { };

    void Start()
    {
        SetActiveIndex(DefaultIndex);
        OnCanvasSwitch.Invoke();
    }

    private void Update()
    {
        _EditorRefresh();
    }

    /*
    Editor Only:
    Hide Child Switcher elements that are not currently selected in editor
    */
    private void _EditorRefresh()
    {
#if UNITY_EDITOR
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
                    || Selection.activeGameObject.transform.IsChildOf(childTransform);
                    childTransform.gameObject.SetActive(selected);
                }
                return;
            }
            // Keep "DefaultIndex" in range
            else if (Selection.activeGameObject.transform != this.transform){
                if (DefaultIndex < 0){
                    DefaultIndex = 0;
                }
                else if (DefaultIndex > this.transform.childCount){
                    DefaultIndex = this.transform.childCount - 1;
                }
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == DefaultIndex);
        }
#endif
    }

    private void _Refresh()
    {
        if (Application.isPlaying)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(i == _ActiveIndex);
            }
        }
    }

    public void SetActiveIndex(int newIndex)
    {
        _ActiveIndex = newIndex;
        _Refresh();
        OnCanvasSwitch.Invoke();
    }

}
