using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    // Start is called before the first frame update
    void Start()
    {
        _ShipInit();
    }

    // Update is called once per frame
    void Update()
    {
        _ShipSetRigidbody();
        _HandleMoveMent();
        _HandleFire();
    }

    private void _HandleMoveMent(){
        var controller = PlayerController.GetController(0);
        Move(controller.MoveVector);
    }

    private void _HandleFire(){
        var controller = PlayerController.GetController(0);
        if (controller.Fire > 0){
            Fire();
        }
    }

    void OnDrawGizmos(){
        var BodyBounds = new RigidBody2DBounds(_ShipRigidbody);
        BodyBounds.DrawDebugGizmos();
    }
}
