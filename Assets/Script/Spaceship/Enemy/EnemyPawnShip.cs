using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawnShip : Ship
{
    public Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = Player.Get();
        _ShipInit();
    }

    // Update is called once per frame
    void Update()
    {
        _ShipSetRigidbody();
        Move(Vector3.down);

        if (transform.position.y < -GameCamera.TargetRatio.y ){
            GameObject.Destroy(gameObject);
        }
    }
}
