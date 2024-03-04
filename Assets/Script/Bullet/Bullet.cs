using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(0,1.0f)]
    public bool Pierce = false;
    public float DamageValue;
    public Rigidbody2D _Rigibody;
    public Vector2 Direction;
    public float Speed;

    [Range(1.0f,5.0f)]
    public float MaxLifeTime;
    private float _LifeTime;
    
    public string[] FriendlyTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _SetBulletRigidbody(); 
        _BulletMove();
        _BulletTick();
    }

    protected void _BulletTick(){
        _LifeTime += Time.deltaTime;
        if (_LifeTime >= MaxLifeTime){
            GameObject.Destroy(gameObject);
        }
    }

    protected void _SetBulletRigidbody(){
        if (_Rigibody){
            _Rigibody.gravityScale = 0;
            _Rigibody.isKinematic = true;
            _Rigibody.useFullKinematicContacts = true;
        }
    }

    protected void _BulletMove(){
        _Rigibody.MovePosition(
            _Rigibody.transform.position + ((Vector3)Direction * Speed * Time.deltaTime)
        );
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.rigidbody){
            var otherGameobject = col.rigidbody.gameObject;
            var character = otherGameobject.GetComponent<Character>();
            if (character){
                if (!FriendlyTag.Contains(character.CharacterTag)){
                    character.Damage(DamageValue);
                    if (!Pierce) GameObject.Destroy(gameObject);
                }
            }
        }
    }
}
