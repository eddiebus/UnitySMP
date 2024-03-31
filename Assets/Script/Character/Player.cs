using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player : Character
{
    public static int Score;
    public float InvisibilityTime = 0;

    void Start(){
        this.OnDamage.AddListener( ()=> {
            this.InvisibilityTime += 3;
        });
    }

    // Update is called once per frame
    void Update()
    {
        CharacterTag = CharacterNames.Player;
        if (InvisibilityTime > 0)
        {
            InvisibilityTime -= Time.deltaTime;
            this.Invincible = true;
        }
        else
        {
            this.Invincible = false;
        }
    }

    public static Player Get()
    {
        return FindFirstObjectByType<Player>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var otherBody = col.rigidbody;
        var enemyComp = otherBody.gameObject.GetComponent<Enemy>();

        if (enemyComp)
        {
            this.Damage(0.2f);
            Debug.Log($"Player has hit Enemy {enemyComp.gameObject.name}");
        }
    }

}


