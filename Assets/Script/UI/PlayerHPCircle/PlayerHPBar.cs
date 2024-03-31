using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
UI - PlayerHP Bar
Circle HP Bar
*/
public class PlayerHPBar : MonoBehaviour
{
    protected Material MainMat;
    public CanvasGroup canvasGroup;
    [Range(0.01f, 0.4f)]
    public float Width;
    public Image MainBarObject_Image;

    public float DisplayValue = 0.0f;
    public float Opacity = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

        canvasGroup = GetComponent<CanvasGroup>();

        MainMat = new Material(
            Shader.Find("Unlit/UVCircle")
        );
        MainMat.SetFloat("_MaxRadius", 0.5f + Width);
        MainMat.SetFloat("_MinRadius", 0.5f - Width);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Get())
        {
            
            Player currentPlayer = Player.Get();

            DisplayValue = Mathf.Lerp(
                DisplayValue,
                currentPlayer.Health,
                Time.deltaTime * 3
            );


            MainMat.SetFloat("_MaxRadius", 0.5f + Width);
            MainMat.SetFloat("_MinRadius", 0.5f - Width);
            MainBarObject_Image.material = MainMat;
            MainBarObject_Image.material.SetFloat("_Angle", DisplayValue);

            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha,
            currentPlayer.InvisibilityTime,
            Time.deltaTime * 5);

            

            if (GameCamera.Get()){
                Vector3 newPos =  GameCamera.Get().CamComponent.WorldToScreenPoint(
                    currentPlayer.transform.position
                    );
                this.transform.position = newPos;
            }
        }
    }
}
