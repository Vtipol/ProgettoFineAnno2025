using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //Static For Easy Ref.
    public static PlayerInput PlayerInput;

    public static Vector2 Movement;
    public static bool JumpWasPressed;
    public static bool JumpIsHeld;
    public static bool JumpWasRelesed;
    public static bool RunIsHeld;
    public static bool TransformationIsPressed;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;
    private InputAction _attackAction;
    private InputAction _transformAction;

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();

        _moveAction = PlayerInput.actions["Move"];
        _jumpAction = PlayerInput.actions["Jump"];
        _runAction = PlayerInput.actions["Sprint"];
        _attackAction = PlayerInput.actions["Attack"];
        _transformAction = PlayerInput.actions["Transformation"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        RunIsHeld = _runAction.IsPressed();

        JumpWasPressed = _jumpAction.WasPerformedThisFrame();
        JumpIsHeld = _jumpAction.IsPressed();
        JumpWasRelesed = _jumpAction.WasReleasedThisFrame();

        TransformationIsPressed = _transformAction.IsPressed();
    }
}
