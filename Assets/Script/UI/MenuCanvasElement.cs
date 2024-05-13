using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuCanvasElement : MonoBehaviour, IPointerEnterHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void TakeFocus(){
        var parentMenuCanvas = GetComponentInParent<MenuCanvas>();
        
        if (parentMenuCanvas != null) {
            if (Application.isPlaying)
            {
                EventSystem.current.SetSelectedGameObject(this.gameObject);
            }
        }
    }

    public void OnPointerEnter(PointerEventData PointerEventData){
        Debug.Log($"Hello Mouse event from {this.gameObject.name}");
        TakeFocus();
    }

    void OnMouseEnter(){
        Debug.Log($"Hello Mouse event from {this.gameObject.name}");
    }
}
