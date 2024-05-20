using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;



public enum HPBarDirection
{
    Forward,
    Backward,
    Center
}
public class HPBar : MonoBehaviour
{
    public HPBarDirection Direction;
    public float Value = 1.0f;
    private float DisplayValue = 0.0f;
    public float Speed;



    private RectTransform rectTransform = null;
    private RectTransform parentRectTransform = null;

    protected void HPBarInit()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        parentRectTransform = gameObject.transform.parent.GetComponent<RectTransform>();
        DisplayValue = Value;
    }
    void Awake()
    {
        HPBarInit(); 
    }

    // Update is called once per frame
    void Update()
    {
        _HPBarUpdate();
    }

    protected void _HPBarUpdate()
    {
        _HPBarUpdateSize();
        _HPBarClampValue();
        _HPBarUpdateValue();
    }
    
    protected void _HPBarUpdateSize()
    {
        if (!rectTransform || !parentRectTransform) return;
        else
        {

            float barWidth = parentRectTransform.rect.width;
            float halfbarWidth = parentRectTransform.rect.width / 2.0f;

            switch (Direction)
            {
                case HPBarDirection.Forward:
                    {
                        rectTransform.offsetMax = new Vector2(
                        -barWidth + (DisplayValue * barWidth),
                        0
                        );
                        rectTransform.offsetMin = new Vector2(0, 0);
                        break;
                    }
                case HPBarDirection.Backward:
                    {
                        rectTransform.offsetMax = new Vector2(
                        0,
                        0
                        );
                        rectTransform.offsetMin = new Vector2(
                            barWidth - (DisplayValue * barWidth),
                            0);
                        break;
                    }
                case HPBarDirection.Center:
                    {
                        rectTransform.offsetMax = new Vector2(
                        -halfbarWidth + (DisplayValue * halfbarWidth),
                        0
                        );
                        rectTransform.offsetMin = new Vector2(
                            halfbarWidth - (DisplayValue * halfbarWidth), 
                            0);
                        break;
                    }
                default:
                    break;
            }
            
        }
    }

    protected void _HPBarClampValue()
    {
        if( Value < 0)
        {
            Value = 0;
        }
        else if( Value > 1)
        {
            Value = 1;
        }

        if (Speed < 0)
        {
            Speed = 0.1f;
        }
    }

    protected void _HPBarUpdateValue()
    {
        DisplayValue = Mathf.Lerp(DisplayValue, Value, Speed * Time.deltaTime);
    }
}
