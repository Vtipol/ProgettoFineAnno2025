using UnityEngine;

public class TrampolineState : BuddyState
{
    public override void OnEnter()
    {
        Debug.Log("Entered TrampolineState");
        controller.EnableOnlyCollider(controller.capsuleCollider);
    }
    public override void OnUpdate()
    {
       
            if (InputManager.TransformationIsPressed == true && !pickedUp.IsPickedUp)
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
        controller.EnableOnlyCollider(null);
    }
}
