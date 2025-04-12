using UnityEngine;
using UnityEngine.InputSystem;

public class GumState : BuddyState, IInputHandler
{
    public override void OnEnter()
    {
        Debug.Log("Entered GumState");
        controller.EnableOnlyCollider(controller.circleCollider);
        followPlayer.IsTrasformed = true;
    }
    public override void OnUpdate()
    {
        if (!followPlayer.targetInView)
        {
            Debug.Log("Target is OOR");
            TeleportToPlayer();
        }
    }
    public void OnInput(string input)
    {
        
            switch (input)
            {
                case "E":
                    controller.SwitchState(controller.neutralState);
                    break;
                case "Q":
                    controller.SwitchState(controller.soapState);
                    break;
            }
        
    }
    public override void OnExit()
    {
        controller.EnableOnlyCollider(null);
    }
}
