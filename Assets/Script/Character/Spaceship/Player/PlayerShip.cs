using UnityEngine;


public enum PlayerShipMode
{
    Player,
    AI,
    None
}
public class PlayerShip : Ship
{
    public PlayerShipMode ShipMode = PlayerShipMode.None;
    // Start is called before the first frame update
    void Start()
    {
        _ShipInit();
    }

    // Update is called once per frame
    void Update()
    {
        _ShipSetRigidbody();

        switch (ShipMode)
        {
            case PlayerShipMode.Player:
                {
                    _HandleMoveMent();
                    _ShipFixPositionWithinCamera(CameraBoundFixAxis.Both);
                    _HandleFire();
                    break;
                }
            case PlayerShipMode.AI:
                break;
            default:
                break;
        }
    }

    private void _HandleMoveMent()
    {
        var controller = PlayerController.GetController(0);
        Move(controller.MoveVector);
    }

    private void _HandleFire()
    {
        var controller = PlayerController.GetController(0);
        if (controller.Fire > 0)
        {
            Fire();
        }
    }

    void OnDrawGizmos()
    {
        if (_ShipRigidbody)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(ShipBounds.center, ShipBounds.size);
        }
    }
}
