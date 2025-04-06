using UnityEngine;

public class SoapState : BuddyState
{
    public override void OnEnter()
    {
        Debug.Log("Entered SoapState");
    }
    public override void OnUpdate()
    {
        if (InputManager.TransformationIsPressed == true)
        {
            followPlayer.IsTrasformed = true;
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                controller.SwitchState(controller.gumState);
            }
            else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                controller.SwitchState(controller.trampolineState);
            }
        }
        if (!followPlayer.targetInView)
        {
            Debug.Log("Target is OOR");
            TeleportToPlayer();
        }
    }
    public override void OnExit()
    {
     
    }
}
