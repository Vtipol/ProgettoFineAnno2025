using UnityEngine;

public class NeutralState : BuddyState
{
    public override void OnEnter()
    {
        Debug.Log("Entered Neutral State");
        followPlayer.IsTrasformed = false;
    }
    public override void OnUpdate()
    {
        if (InputManager.TransformationIsPressed == true)
        {

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                controller.SwitchState(controller.trampolineState);
            }
            else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                controller.SwitchState(controller.gumState);
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
