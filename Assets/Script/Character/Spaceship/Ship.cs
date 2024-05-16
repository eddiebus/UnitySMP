using Unity.VisualScripting;
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
            if (ShipBounds.min.y < playarea.min.y)
            {
                float difference = Mathf.Abs(
                    playarea.min.y - ShipBounds.min.y
                    );

                Debug.Log($"Differece y =  {difference}");

                transform.position = new Vector3(
                    _ShipRigidbody.position.x,
                    _ShipRigidbody.position.y + Mathf.Abs(difference),
                    this.transform.position.z
                    );
            }

            else if (ShipBounds.max.y > playarea.max.y)
            {
                float difference = Mathf.Abs(
                    playarea.max.y - ShipBounds.max.y
                    );

                transform.position = new Vector3(
                    _ShipRigidbody.position.x,
                    _ShipRigidbody.position.y - Mathf.Abs(difference),
                    this.transform.position.z
                    );
            }
        }

        if (axis == CameraBoundFixAxis.Vertical || axis == CameraBoundFixAxis.Both)
        {
            if (ShipBounds.min.x < playarea.min.x)
            {
                float difference = Mathf.Abs(
                    playarea.min.x - ShipBounds.min.x
                    );

                transform.position = new Vector3(
                    _ShipRigidbody.position.x + Mathf.Abs(difference),
                    _ShipRigidbody.position.y,
                    this.transform.position.z
                    );
            }
            else if (
                ShipBounds.max.x > playarea.max.x
            )
            {
                float difference = Mathf.Abs(
                    playarea.max.x - ShipBounds.max.x
                    );

                transform.position = new Vector3(
                    _ShipRigidbody.position.x - Mathf.Abs(difference),
                    _ShipRigidbody.position.y,
                    this.transform.position.z
                    );
            }

        }
    }

    protected void _ShipSetRigidbody()
    {
        _ShipRigidbody.isKinematic = true;
        _ShipRigidbody.useFullKinematicContacts = true;
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
            foreach (var gun in _Guns)
            {
                gun.Fire();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _ShipInit();
    }
}
