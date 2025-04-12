using UnityEngine;

public class TrampolineState : BuddyState, IInputHandler
{
    public override void OnEnter()
    {
        Debug.Log("Entered TrampolineState");
        controller.EnableOnlyCollider(controller.capsuleCollider);
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
                    controller.SwitchState(controller.soapState);
                    break;
                case "Q":
                    controller.SwitchState(controller.neutralState);
                    break;
            }
        
    }
    public override void OnExit()
    {
        controller.EnableOnlyCollider(null);
    }
}
