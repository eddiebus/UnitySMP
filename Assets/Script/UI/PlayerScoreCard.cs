using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public TMP_Text TextComponent;
    [Range(1,5)]
    public float ChangeSpeed;

    protected float _DisplayScore = 0;
    protected float InitTextSize;
    // Start is called before the first frame update
    void Start()
    {
        InitTextSize = TextComponent.fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        _DisplayScore = Mathf.Lerp(_DisplayScore, Player.Score,ChangeSpeed * Time.deltaTime * 10);
        _DisplayScore = Mathf.Ceil(_DisplayScore);
        TextComponent.text = GetScoreString();
    }

    protected string GetScoreString(){
        string resultString = ((int)_DisplayScore).ToString();

        int stepCount = 0;
        for (int i = resultString.Length - 1; i > 0; i--){
            stepCount++;
            if (stepCount == 3 && resultString.Length >= 4){
                resultString = resultString.Insert(i,",");
                i--;
                stepCount = 0;
            }
        }
        
        return resultString;

        
    }
}
