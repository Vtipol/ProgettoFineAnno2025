using UnityEngine;
using UnityEngine.InputSystem;

public class GumState : BuddyState
{
    public override void OnEnter()
    {
        Debug.Log("Entered GumState");
    }
    public override void OnUpdate()
    {
        if (InputManager.TransformationIsPressed == true)
        {
            followPlayer.IsTrasformed = true;
            followPlayer.Deactivate();
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                controller.SwitchState(controller.neutralState);
            }
            else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                controller.SwitchState(controller.soapState);
            }
        }
    }
    public override void OnExit()
    {
      
    }
}
