using UnityEngine;
using UnityEngine.UI;


/*
UI - PlayerHP Bar
Circle HP Bar
*/
public class PlayerHPBar : MonoBehaviour
{
    public Material MainMat;
    public CanvasGroup canvasGroup;
    public Image MainBarObject_Image;

    public float DisplayValue = 1.0f;
    protected float Opacity = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        MainMat = Object.Instantiate(MainMat);
        canvasGroup = GetComponent<CanvasGroup>();

        Player currentPlayer = Player.Get();
        if (currentPlayer)
        {
            currentPlayer.OnDamage.AddListener( () => {
                Opacity = 5.0f;
            });
        }
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
            MainBarObject_Image.material = MainMat;
            MainBarObject_Image.material.SetFloat("_Fill", DisplayValue);


            Opacity -= Time.deltaTime;
            canvasGroup.alpha = Opacity;
            GameCamera targetGameCam = GameCamera.Get();

            if (targetGameCam)
            {

                RectTransform myTransform = GetComponent<RectTransform>();
                Vector2 anchroPoint = Vector2.zero;
                myTransform.anchorMin = anchroPoint;
                myTransform.anchorMax = anchroPoint;
                RectTransform parentTrasnform = myTransform.parent.gameObject.GetComponent<RectTransform>();
                Vector3 screenPos = targetGameCam.CamComponent.WorldToViewportPoint(
                    currentPlayer.transform.position
                    );

                Vector3 newPos = Vector2.zero;
                newPos.x = screenPos.x * parentTrasnform.sizeDelta.x;
                newPos.y = screenPos.y * parentTrasnform.sizeDelta.y;
                myTransform.anchoredPosition = newPos;
            }
        }
    }
}