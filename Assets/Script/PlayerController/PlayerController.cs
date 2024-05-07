using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;


// Mode of Input
public enum PlayerConState
{
    KeyboardMouse,
    Gamepad,
    Touch
}

public struct PlayerConSetting
{
    public float MouseSensitivity;
    public float GamepadSensitivity;
    public float GamepadDeadZone;
    public float TriggerDeadZone;
}

// New Player Controller with Static Methods
public class PlayerController
{
    private static PlayerConSetting settings = new PlayerConSetting();
    private static PlayerController[] _Instances;
    private int _ControllerIndex = -1;
    private PlayerConState _ControllerState = PlayerConState.KeyboardMouse;
    public PlayerConState ControllerState => _ControllerState;
    public Vector2 MoveVector => _MoveVector;
    public float Fire => _Fire;

    private Vector2 _MoveVector;
    private Vector2 _AimVector;
    private Vector2 _AimPoint = new Vector2(0.5f, 0.5f);
    private float _Fire;

    public static PlayerController GetController(int Index)
    {
        if (_Instances == null)
        {
            _InitController();
        }
        _Instances[Index]._UpdateValues();
        return _Instances[Index];
    }


    public static void SetSettings(PlayerConSetting newSettings)
    {
        settings = newSettings;
    }

    private static void _InitController()
    {
        _Instances = new PlayerController[]{
            new PlayerController(0),
            new PlayerController(1),
            new PlayerController(2),
            new PlayerController(3)
        };
    }

    private PlayerController(int PlayerIndex)
    {
        _UpdateValues();
        _ControllerIndex = PlayerIndex;
        InputSystem.onEvent += (eventP, device) =>
        {
            this._CheckState(eventP, device);
        };

    }

    private void _UpdateValues()
    {
        _MoveVector = Vector2.zero;
        _AimVector = Vector2.zero;
        switch (_ControllerState)
        {
            case PlayerConState.KeyboardMouse:
                {
                    // Get Input based on Active (most recent) Device
                    Keyboard _keyboard = Keyboard.current;
                    Mouse _mouse = Mouse.current;

                    var keyMoveVector = Vector2.zero;
                    var up = _keyboard.wKey.isPressed;
                    var down = _keyboard.sKey.isPressed;
                    var left = _keyboard.aKey.isPressed;
                    var right = _keyboard.dKey.isPressed;

                    if (up && !down)
                    {
                        keyMoveVector += Vector2.up;
                    }
                    else if (down && !up)
                    {
                        keyMoveVector += Vector2.down;
                    }

                    if (left && !right)
                    {
                        keyMoveVector += Vector2.left;
                    }
                    else if (right && !left)
                    {
                        keyMoveVector += Vector2.right;
                    }

                    _MoveVector = keyMoveVector;
                    if (_mouse.leftButton.isPressed || _keyboard.jKey.isPressed)
                    {
                        _Fire = 1.0f;
                    }
                    else
                    {
                        _Fire = 0.0f;
                    }
                    break;
                }
            case PlayerConState.Gamepad:
                {
                    Gamepad _gamepad = GetGamepad();
                    var leftStick = _gamepad.leftStick.ReadValue();
                    var rightStick = _gamepad.rightStick.ReadValue();
                    var rightTrigger = _gamepad.rightTrigger.ReadValue();
                    var leftTrigger = _gamepad.leftTrigger.ReadValue();

                    var up = _gamepad.dpad.up.isPressed;
                    var down = _gamepad.dpad.down.isPressed;
                    var left = _gamepad.dpad.left.isPressed;
                    var right = _gamepad.dpad.right.isPressed;

                    _AimPoint += rightStick * Time.deltaTime;
                    if (leftStick.magnitude > 0.1f)
                    {
                        _MoveVector = leftStick;
                    }
                    else
                    {
                        if (up)
                        {
                            _MoveVector += Vector2.up;
                        }
                        else if (down)
                        {
                            _MoveVector += Vector2.down;
                        }

                        if (left)
                        {
                            _MoveVector += Vector2.left;
                        }
                        else if (right)
                        {
                            _MoveVector += Vector2.right;
                        }
                    }
                    float triggerDeadZone = 0.1f;
                    if (rightTrigger >= triggerDeadZone)
                    {
                        _Fire = rightTrigger;
                    }
                    else
                    {
                        _Fire = 0.0f;
                    }
                    break;
                }
            case PlayerConState.Touch:
                {
                    TouchControl primaryTouch = Touchscreen.current.touches[0];
                    TouchControl secondaryTouch = Touchscreen.current.touches[1];

                    
                    if (primaryTouch.press.ReadValue() > 0.0f) {
                        
                        Vector2 StartPos = primaryTouch.startPosition.ReadValue();
                        Vector2 EndPos = primaryTouch.position.ReadValue();

                        _MoveVector = (EndPos - StartPos) / 200.0f;
                        
                        
                        Debug.Log($"Primary Touch Pos = {_MoveVector}");
                    }

                    if (secondaryTouch.press.ReadValue() > 0.0f ){
                        _Fire = secondaryTouch.press.ReadValue();
                    }
                    else{
                        _Fire = 0.0f;
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }

        // Clamp AimPoint
        float Limit = 1.0f;
        if (_AimPoint.x > Limit)
        {
            _AimPoint.x = Limit;
        }
        else if (_AimPoint.x < -Limit)
        {
            _AimPoint.x = -Limit;
        }

        if (_AimPoint.y > Limit)
        {
            _AimPoint.y = Limit;
        }
        else if (_AimPoint.y < -Limit)
        {
            _AimPoint.y = -Limit;
        }

    }

    // Get gamepad assigned to this player controller
    public Gamepad GetGamepad()
    {
        if (_ControllerIndex < Gamepad.all.Count)
        {
            return Gamepad.all[_ControllerIndex];
        }
        else
        {
            return null;
        }
    }

    private void _CheckState(InputEventPtr @event, InputDevice inputDevice)
    {
        if (inputDevice is Keyboard)
        {
            _ControllerState = PlayerConState.KeyboardMouse;
        }
        else if (inputDevice is Gamepad)
        {
            // Change to gamepad control if gamepad match
            if ((Gamepad)inputDevice == GetGamepad())
                _ControllerState = PlayerConState.Gamepad;
        }
        else if (inputDevice is Touchscreen)
        {
            _ControllerState = PlayerConState.Touch;
            Debug.Log("Received touch input");
        }
    }

}
