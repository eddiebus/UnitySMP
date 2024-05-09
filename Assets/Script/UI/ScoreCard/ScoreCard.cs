using Unity.VisualScripting;
using UnityEngine;



public class ScoreCard : MonoBehaviour
{
    public Animator AnimatorComponent;
    public TMPro.TMP_Text TextComponent;
    [Range(1, 5)]
    public float ChangeSpeed;

    protected int _ScoreTarget;
    protected float _DisplayScore = 0;
    protected float InitTextSize;

    const string AnimControl_ScoreChanged = "ScoreChange";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplayScore();
        UpdateAnimatorController();
    }

    protected string GetScoreString()
    {
        string resultString = ((int)Mathf.Ceil(_DisplayScore)).ToString();
        int stepCount = 0;
        for (int i = resultString.Length - 1; i > 0; i--)
        {
            stepCount++;
            if (stepCount == 3 && resultString.Length >= 4)
            {
                resultString = resultString.Insert(i, ",");
                i--;
                stepCount = 0;
            }
        }
        return resultString;
    }


    protected void UpdateDisplayScore(){
        _DisplayScore = Mathf.Lerp(_DisplayScore, _ScoreTarget, ChangeSpeed * Time.deltaTime * 2);
        _DisplayScore = Mathf.Ceil(_DisplayScore);
        TextComponent.text = GetScoreString();
        UpdateAnimatorController();
    }

    protected void UpdateAnimatorController()
    {
        if (!AnimatorComponent) return;
        AnimatorComponent.SetBool(
            AnimControl_ScoreChanged, (int)_DisplayScore != _ScoreTarget
        );
    }

    
}
