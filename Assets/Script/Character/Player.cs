using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public static int Score;
    // Update is called once per frame
    void Update()
    {
        CharacterTag = CharacterNames.Player;   
    }

    public static Player Get(){
        return FindFirstObjectByType<Player>();
    }
}
