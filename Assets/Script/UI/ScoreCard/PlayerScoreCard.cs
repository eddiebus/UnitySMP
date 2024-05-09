using TMPro;
using
/* Unmerged change from project 'Assembly-CSharp.Player'
Before:
using UnityEngine;
using TMPro;
After:
using TMPro;
using UnityEngine;
*/
UnityEngine;

public class PlayerScore : ScoreCard
{
    const string AnimControl_ScoreChanged = "ScoreChange";
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _ScoreTarget = Player.Score;
        UpdateDisplayScore();
        UpdateAnimatorController();
    }

    
}
