using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    InputSystem_Actions actionMap;
    private Vector2 MoveInput;
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody rb;
    private bool isGrounded;

    void InputSystem_Actions.IPlayerActions.OnAttack(InputAction.CallbackContext context)
    {}
    void InputSystem_Actions.IPlayerActions.OnCrouch(InputAction.CallbackContext context)
    {}
    void InputSystem_Actions.IPlayerActions.OnInteract(InputAction.CallbackContext context)
    {}
    void InputSystem_Actions.IPlayerActions.OnLook(InputAction.CallbackContext context)
    {}
    void InputSystem_Actions.IPlayerActions.OnNext(InputAction.CallbackContext context)
    {}
    void InputSystem_Actions.IPlayerActions.OnPrevious(InputAction.CallbackContext context)
    {}
    void InputSystem_Actions.IPlayerActions.OnSprint(InputAction.CallbackContext context)
    {}
    void InputSystem_Actions.IPlayerActions.OnJump(InputAction.CallbackContext context)
    {}
    private void Awake()
    {
        actionMap = new InputSystem_Actions();
        actionMap.Player.SetCallbacks(this);
        rb = GetComponent<Rigidbody>(); // Make sure your player has a Rigidbody component
    }

    private void OnEnable()
    {
        actionMap.Player.Enable();
    }

    private void OnDisable()
    {
        actionMap.Player.Disable();
    }

    private void Update()
    {
        Move();
        CheckGrounded();
    }

    public void Move()
    {
        Vector3 moveDirection = new Vector3(MoveInput.x, 0, MoveInput.y);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
    
    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }
}