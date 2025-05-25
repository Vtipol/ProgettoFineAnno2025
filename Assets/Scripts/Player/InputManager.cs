using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //Static For Easy Ref.
    public static PlayerInput PlayerInput;
    public Player Player;

    public static Vector2 Movement;
    public static bool JumpWasPressed;
    public static bool JumpIsHeld;
    public static bool JumpWasRelesed;
    public static bool RunIsHeld;
    public static bool AttackIsPressed;
    public static bool AttackDownExecuted;
    public static bool TransformLeftWasPressed;
    public static bool TransformRightWasPressed;
    public static bool TeleportWasPressed;
    public static bool InteractWasPressed;

    public static bool DownisHeld;


    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;
    private InputAction _attackAction;
    private InputAction _downInputAction;
    private InputAction _transformLeftAction;
    private InputAction _transformRightAction;
    private InputAction _teleportAction;
    private InputAction _interactAction;
    
    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();

        _moveAction = PlayerInput.actions["Move"];
        _jumpAction = PlayerInput.actions["Jump"];
        _runAction = PlayerInput.actions["Sprint"];
        _attackAction = PlayerInput.actions["Attack"];
        _downInputAction = PlayerInput.actions["DownInput"];

        _transformLeftAction = PlayerInput.actions["TransformLeft"];
        _transformRightAction = PlayerInput.actions["TransformRight"];
        _teleportAction = PlayerInput.actions["Teleport"];

        _interactAction = PlayerInput.actions["Interact"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        RunIsHeld = _runAction.IsPressed();

        JumpWasPressed = _jumpAction.WasPerformedThisFrame();
        JumpIsHeld = _jumpAction.IsPressed();
        JumpWasRelesed = _jumpAction.WasReleasedThisFrame();

        AttackIsPressed = _attackAction.WasPressedThisFrame();
        AttackDownExecuted = _attackAction.WasPressedThisFrame() && !Player._isGrounded;

        TransformLeftWasPressed = _transformLeftAction.WasPerformedThisFrame();
        TransformRightWasPressed = _transformRightAction.WasPerformedThisFrame();
        TeleportWasPressed = _teleportAction.WasReleasedThisFrame();

        InteractWasPressed = _interactAction.WasPressedThisFrame();

        DownisHeld = _downInputAction.IsPressed();
    }
}
