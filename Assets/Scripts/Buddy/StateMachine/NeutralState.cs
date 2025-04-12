using UnityEngine;

public class NeutralState : BuddyState, IInputHandler
{
    public override void OnEnter()
    {
        Debug.Log("Entered Neutral State");
        followPlayer.IsTrasformed = false;
        controller.EnableOnlyCollider(null);
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
                    controller.SwitchState(controller.trampolineState);
                    break;
                case "Q":
                    controller.SwitchState(controller.gumState);
                    break;
            }
        
    }
    public override void OnExit()
    {
       
    }
}
