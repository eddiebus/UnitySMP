using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CameraBoundFixAxis
{
    Vertical,
    Horizontal,
    Both
}
public class Ship : MonoBehaviour
{
    public float MoveSpeed;
    protected Rigidbody2D _ShipRigidbody;
    public ShipGun[] _Guns;

    public Bounds ShipBounds => RigidBody2DBounds.Get(_ShipRigidbody);
    protected void _ShipInit()
    {
        _ShipRigidbody = GetComponent<Rigidbody2D>();
        _Guns = GetComponentsInChildren<ShipGun>();
    }

    // Keep the ship withing bounds of camera on certain axis
    protected void _ShipFixPositionWithinCamera(CameraBoundFixAxis axis)
    {
        Bounds playarea = GameCamera.Get().GetBounds();
        if (axis == CameraBoundFixAxis.Vertical || axis == CameraBoundFixAxis.Both)
        {
            if ((ShipBounds.center.y - ShipBounds.extents.y) < (playarea.center.y - playarea.extents.y))
            {
                float diff =
                (playarea.center.y - playarea.extents.y) -
                (ShipBounds.center.y - ShipBounds.extents.y);

                _ShipRigidbody.position = _ShipRigidbody.position + (Vector2.up * diff);
            }
            else if (
                (ShipBounds.center.y + ShipBounds.extents.y) >
                (playarea.center.y + playarea.extents.y)
            )
            {
                float diff =
                (playarea.center.y + playarea.extents.y) -
                (ShipBounds.center.y + ShipBounds.extents.y);
                _ShipRigidbody.position = _ShipRigidbody.position - (Vector2.down * diff);
            }
        }

        if (axis == CameraBoundFixAxis.Vertical || axis == CameraBoundFixAxis.Both)
        {
            if ((ShipBounds.center.x - ShipBounds.extents.x) < (playarea.center.x - playarea.extents.x))
            {
                float diff =
                (ShipBounds.center.x - ShipBounds.extents.x) -
                 (playarea.center.x - playarea.extents.x);

                _ShipRigidbody.position = _ShipRigidbody.position - (Vector2.right * diff);
            }
            else if (
                (ShipBounds.center.x + ShipBounds.extents.x) >
                (playarea.center.x + playarea.extents.x)
            )
            {
                float diff =
                (ShipBounds.center.x + ShipBounds.extents.x) -
                 (playarea.center.x + playarea.extents.x);

                _ShipRigidbody.position = _ShipRigidbody.position + (Vector2.left * diff);
            }

        }
    }

    protected void _ShipSetRigidbody()
    {
        _ShipRigidbody.isKinematic = true;
    }

    public void Move(Vector2 moveVector)
    {
        _ShipRigidbody.MovePosition(_ShipRigidbody.position + (moveVector * MoveSpeed * Time.deltaTime));
    }

    public void MoveTo(Vector3 TargetPosition)
    {
    }

    public void Fire()
    {
        if (_Guns.Length > 0)
        {
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
