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
        _ShipFixPositionWithinCamera(CameraBoundFixAxis.Both);
        _HandleFire();
    }

    // Keep ship in bounds of GameCamera
    private void _FixPosition()
    {
        Bounds playarea = GameCamera.Get().GetBounds();

        Vector3 down = ShipBounds.center + (Vector3.down * ShipBounds.size.y);
        Vector3 up = ShipBounds.center + (Vector3.up * ShipBounds.size.y);

        Vector3 left = ShipBounds.center + (Vector3.left * ShipBounds.size.x);
        Vector3 right = ShipBounds.center + (Vector3.right * ShipBounds.size.x);

        if ((ShipBounds.center.y - ShipBounds.extents.y) < (playarea.center.y - playarea.extents.y))
        {
            float diff =
            (playarea.center.y - playarea.extents.y) -
            (ShipBounds.center.y - ShipBounds.extents.y);

            Debug.Log($"Y Diff = {diff}");

            _ShipRigidbody.position = _ShipRigidbody.position + (Vector2.up * diff);
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
