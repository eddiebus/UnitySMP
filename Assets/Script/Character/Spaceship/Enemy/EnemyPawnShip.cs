using UnityEngine;

public class EnemyPawnShip : Ship
{
    public Enemy _EnemyComp;
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

        _ShipFixPositionWithinCamera(CameraBoundFixAxis.Horizontal);

        if (transform.position.y < -GameCamera.TargetRatio.y)
        {
            Destroy(_EnemyComp.GetCharacterRootObj());
        }
    }
}
