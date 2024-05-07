using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class LoadingCanvas : MonoBehaviour
{
    protected static float TargetAlpha = 1.0f;
    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (!canvasGroup) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            if (SceneLoader.IsLoading())
            {
                TargetAlpha += Time.deltaTime * 5.0f;
                if (TargetAlpha >= 0.99f)
                {
                    Debug.Log("Hello");
                    SceneLoader.SwitchToNewScene();
                }
            }
            else
            {
                TargetAlpha -= Time.deltaTime * 1.0f;
            }

            if (TargetAlpha > 1.0f) TargetAlpha = 1.0f;
            else if (TargetAlpha < 0.0f) TargetAlpha = 0.0f;

            canvasGroup.alpha = TargetAlpha;

            canvasGroup.blocksRaycasts = canvasGroup.alpha > 0.4f;
            canvasGroup.interactable = false;
        }

        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.0f;

#if UNITY_EDITOR
            if (Selection.activeGameObject)
            {
                var selectTransform = Selection.activeGameObject.transform;

                bool selected = selectTransform == this.transform
                || selectTransform.IsChildOf(this.transform);
                if (selected)
                {
                    canvasGroup.alpha = 1.0f;
                }
            }
#endif
        }
    }
}
