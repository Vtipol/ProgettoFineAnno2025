using UnityEngine;

public class SoapState : BuddyState, IInputHandler
{
    public override void OnEnter()
    {
        controller.EnableOnlyCollider(controller.boxCollider);
        followPlayer.IsTrasformed = true;
    }
    public override void OnUpdate()
    {
        if (!followPlayer.targetInView)
        {
            TeleportToPlayer();
        }
    }
    public void OnInput(string input)
    {
        
            switch (input)
            {
                case "E":
                    controller.SwitchState(controller.gumState);
                    break;
                case "Q":
                    controller.SwitchState(controller.trampolineState);
                    break;
            }
        
    }
    public override void OnExit()
    {
        controller.EnableOnlyCollider(null);
    }
}
