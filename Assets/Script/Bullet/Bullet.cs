using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [Range(0, 1.0f)]
    public bool Pierce = false;
    public bool Shrink = false;
    public float DamageValue;
    public Rigidbody2D _Rigibody;
    public Vector2 Direction;
    public float Speed;

    [Range(1.0f, 10.0f)]
    public float MaxLifeTime;
    private float _LifeTime;

    public UnityEvent OnBulletDestroy;


    public string[] FriendlyTag;
    // Start is called before the first frame update
    void Start()
    {
        _Rigibody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _SetBulletRigidbody();
        _BulletMove();
        _BulletTick();

        float shrinklifeTime = MaxLifeTime * 0.9f;
        if (Shrink && _LifeTime > shrinklifeTime)
        {
            float scale = (MaxLifeTime - _LifeTime) / (MaxLifeTime - shrinklifeTime);
            this.transform.localScale = Vector3.one * scale;
        }
    }

    protected void _BulletTick()
    {
        _LifeTime += Time.deltaTime;
        if (_LifeTime >= MaxLifeTime)
        {
            GameObject.Destroy(gameObject);
        }
    }

    protected void _SetBulletRigidbody()
    {
        if (_Rigibody)
        {
            _Rigibody.gravityScale = 0;
            _Rigibody.isKinematic = true;
            _Rigibody.useFullKinematicContacts = true;
        }
    }

    protected void _BulletMove()
    {
        _Rigibody.MovePosition(
            _Rigibody.transform.position + ((Vector3)Direction * Speed * Time.deltaTime)
        );
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.rigidbody)
        {
            var otherGameobject = col.rigidbody.gameObject;
            var character = otherGameobject.GetComponent<Character>();
            if (character)
            {
                if (!FriendlyTag.Contains(character.CharacterTag))
                {
                    character.Damage(DamageValue);
                    if (!Pierce)
                    {
                        OnBulletDestroy.Invoke();
                        GameObject.Destroy(gameObject);
                    }
                }
            }
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (_Rigibody)
        {
            var bodyBounds = RigidBody2DBounds.Get(_Rigibody);

            Gizmos.DrawLine(
                _Rigibody.position,
                _Rigibody.position + (Direction * bodyBounds.size.magnitude)
            );
        }
    }
}
