using NUnit.Framework;
using UnityEngine;
using UnityEngine.TerrainUtils;


public enum PlayerShipMode
{
    Player,
    AI,
    None
}

public class PlayerShip : Ship
{
    public Animator AnimComponent = null;
    public string Anim_Steer = "ShipSteer";
    public string Anim_Idle = "ShipIdle";

    public PlayerShipMode ShipMode = PlayerShipMode.None;
    public Player playerComponent;
    public Material shipMaterial;
    public SpriteRenderer shipRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _ShipInit();
        playerComponent = GetComponent<Player>();
        shipRenderer = GetComponentInChildren<SpriteRenderer>();
        shipMaterial = shipRenderer.material;
        AnimComponent = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _ShipSetRigidbody();
        _HandleControlState();
        _UpdateLook();

        Debug.Log($"Ship Idle = {PlayerController.GetController(0).MoveVector.magnitude < 0.0f}");
        AnimComponent.SetFloat(Anim_Steer, PlayerController.GetController(0).MoveVector.x);
        AnimComponent.SetBool(Anim_Idle,
            Mathf.Abs( PlayerController.GetController(0).MoveVector.x ) < 0.2f);

    }

    private void _HandleControlState()
    {
        switch (ShipMode)
        {
            case PlayerShipMode.Player:
                {
                    _HandlePlayerMoveMent();
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

    private void _UpdateLook()
    {
        if (!this.shipRenderer || !this.playerComponent) return;
        else
        {
            Color shipColor = shipRenderer.color;


            // Flash Colour Alpha 
            float alpha = 1.0f;
            if (playerComponent.InvinsibilityTime > 0)
            {
                alpha = 0.0f;
                alpha += (playerComponent.InvinsibilityTime % 0.2f) * 10;
                
            }
            shipColor.a = alpha;
            shipRenderer.color = shipColor;
            shipMaterial.SetColor("_Color", shipColor);
        }
    }

    private void _HandlePlayerMoveMent()
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
