using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float MoveSpeed;
    protected Rigidbody2D _ShipRigidbody;
    public ShipGun[] _Guns;

    public Bounds ShipBounds => RigidBody2DBounds.Get(_ShipRigidbody);
    protected void _ShipInit(){
        _ShipRigidbody = GetComponent<Rigidbody2D>();
        _Guns = GetComponentsInChildren<ShipGun>();
    }

    protected void _ShipSetRigidbody(){
        _ShipRigidbody.isKinematic = true;
    }

    public void Move(Vector2 moveVector){
        _ShipRigidbody.MovePosition(_ShipRigidbody.position + (moveVector * MoveSpeed *  Time.deltaTime) );
    }

    public void MoveTo(Vector3 TargetPosition){
    }

    public void Fire(){
        if (_Guns.Length > 0){
            _Guns[0].Fire();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _ShipInit();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
