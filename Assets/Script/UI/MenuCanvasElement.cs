using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



// Menu Canvas Child Element
// Handles hover for UI Nav
public class MenuCanvasElement : MonoBehaviour, IPointerEnterHandler
{
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
        TakeFocus();
    }
}
