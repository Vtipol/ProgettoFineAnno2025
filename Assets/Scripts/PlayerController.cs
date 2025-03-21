using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
   InputSystem_Actions actionMap;
   private Vector2 MoveInput;
   [SerializeField] public float moveSpeed = 5f;
   public void OnAttack(InputAction.CallbackContext context) { }
   public void OnCrouch(InputAction.CallbackContext context) { }
   public void OnInteract(InputAction.CallbackContext context) { }
   public void OnJump(InputAction.CallbackContext context) { }
   public void OnLook(InputAction.CallbackContext context) { }
   public void OnNext(InputAction.CallbackContext context) { }
   public void OnPrevious(InputAction.CallbackContext context) { }
   public void OnSprint(InputAction.CallbackContext context) { }
   private void Awake()
   {
      actionMap = new InputSystem_Actions();
      actionMap.Player.SetCallbacks(this);
      
   }
   private void OnEnable()
   {
      actionMap.Player.Enable();
   }
   private void OnDisable()
   {
      actionMap.Player.Disable();
   }

   public void Update()
   {
      Move();
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
}
