using UnityEngine;
using UnityEngine.InputSystem;


public enum TouchJoystickType
{
    Move,
    Fire
}

/*
Display Touch Thumbstick
> Controls Animation Controller variable
> Thumbsticks hide and display from Animation
*/
public class UI_TouchJoyStick : MonoBehaviour
{
    public Animator AnimatorComponent;
    public RectTransform rectTransform;
    public TouchJoystickType Type = TouchJoystickType.Move;
    public GameObject ChildJoystick;

    protected int TouchIndex = 0;
    const string AnimControl_Active = "JoystickActive";
    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        AnimatorComponent = GetComponent<Animator>();
    }

    void UpdateAnimatorController()
    {
        if (AnimatorComponent == null) return;
        var controller = PlayerController.GetController(0);
        if (controller.ControllerState == PlayerConState.Touch)
        {
            var isActive = Touchscreen.current.touches[TouchIndex].press.ReadValue() > 0.0;
            AnimatorComponent.SetBool(AnimControl_Active ,isActive &&
                !GameManager.Get().Pause
                ); ;
        }
    }

    protected void _UpdateTouchIndex()
    {
        if (PlayerController.GetController(0) == null) return;
        if (PlayerController.GetController(0).ControllerState == PlayerConState.Touch)
        {
            switch (this.Type)
            {
                case TouchJoystickType.Move:
                    TouchIndex = 0;
                    break;
                case TouchJoystickType.Fire:
                    TouchIndex = 1;
                    break;
                default:
                    break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        _UpdateTouchIndex();
        UpdateAnimatorController();

        var currentCon = PlayerController.GetController(0);
        if (currentCon != null)
        {
            if (currentCon.ControllerState != PlayerConState.Touch)
                return;
            var targetTouch = Touchscreen.current.touches[TouchIndex];
            switch (this.Type)
            {
                case TouchJoystickType.Move:
                    {
                        var startTouchPos = targetTouch.startPosition.ReadValue();
                        var endTouchPos = targetTouch.position.ReadValue();
                        this.transform.position = startTouchPos;
                        if (ChildJoystick != null)
                        {
                            // Base at start position
                            // End stick at current position
                            if (targetTouch.press.ReadValue() > 0.0f)
                            {
                                transform.position = startTouchPos;
                                ChildJoystick.transform.position = endTouchPos;
                            }
                            else
                            {
                                ChildJoystick.transform.position = transform.position;
                            }
                        }
                        break;
                    }
                case TouchJoystickType.Fire:
                    {
                        if (targetTouch.press.ReadValue() > 0.0){
                            transform.position  = targetTouch.startPosition.ReadValue();
                        }
                        break;
                    }
                default:
                    break;
            }

        }
    }
}
