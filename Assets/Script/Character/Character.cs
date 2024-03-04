using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public static class CharacterNames
{
    public static string Player = "Player";
    public static string Enemy = "Enemy";
    public static string Other = "Other";
}

public class Character : MonoBehaviour
{
    public float Health = 1.0f;
    public bool Invincible;
    public string CharacterTag = "EmptyCharacter";
    public static string GameObjectTagName = "Character";

    public AudioClip[] DamageSFX;
    public UnityEngine.Events.UnityEvent OnDeath;
    public UnityEngine.Events.UnityEvent OnDestroy;
    // Start is called before the first frame update
    void Start()
    {
        tag = GameObjectTagName;
    }

    public void Damage(float Ammount)
    {
        if (Invincible) return;
        bool canDie = Health > 0;
        Health -= Ammount;
        if (canDie && Health < 0){
            OnDeath.Invoke();
        }
    }
    
    public static Character[] FindCharactersWithTag(string TargetTag)
    {
        List<Character> returnList = new List<Character>();

        var searchList = GameObject.FindObjectsByType<Character>(FindObjectsSortMode.None);
        foreach (var obj in searchList)
        {
            if (obj.CharacterTag == TargetTag)
            {
                returnList.Add(obj);
            }
        }
        return returnList.ToArray();
    }

    public static Character FindCharacterWithTag(string TargetTag)
    {
        var search = FindCharactersWithTag(TargetTag);
        if (search.Length > 0)
        {
            return search[0];
        }
        else return null;
    }

    public void DestroyObj(GameObject obj) => GameObject.Destroy(obj);
    
    public void Destroy()
    {
        OnDestroy.Invoke();
        GameObject.Destroy(transform.root.gameObject);
    }


}
