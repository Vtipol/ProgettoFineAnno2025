using UnityEngine;

public class TrampolineState : BuddyState
{
    public override void OnEnter()
    {
        Debug.Log("Entered TrampolineState");
    }
    public override void OnUpdate()
    {
       
            if (InputManager.TransformationIsPressed == true)
            {
                followPlayer.IsTrasformed = true;
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5))
                {
                    controller.SwitchState(controller.soapState);
                }
                else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton4))
                {
                    controller.SwitchState(controller.neutralState);
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
