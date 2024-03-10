using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class CharacterNames
{
    public static string Player = "Player";
    public static string Enemy = "Enemy";
    public static string Other = "Other";
}

public class Character : MonoBehaviour
{
    public GameObject CharacterRoot; // Root Object of Character
    public float Health = 1.0f;
    public bool Invincible;
    public string CharacterTag = "EmptyCharacter";
    public static string GameObjectTagName = "Character";

    public UnityEvent OnDamage;
    public UnityEvent OnDestroy;

    public GameObject GetCharacterRootObj()
    {
        if (CharacterRoot) return CharacterRoot;
        else return this.gameObject;
    }

    public void Damage(float Ammount)
    {
        if (Invincible) return;
        Health -= Ammount;
        OnDamage.Invoke();
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
        GameObject.Destroy(this.GetCharacterRootObj());
    }

    // Destroy Character without invoking event
    public void DestroySilent()
    {
        GameObject.Destroy(this.GetCharacterRootObj());
    }

    public void DestroyChildren()
    {
        var childCharacter = GetComponentsInChildren<Character>();
        foreach (var character in childCharacter)
        {
            if (character != this)
            {
                character.Destroy();
            }
        }
    }
}
