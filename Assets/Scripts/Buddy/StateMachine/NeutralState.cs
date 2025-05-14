using UnityEngine;

public class NeutralState : BuddyState, IInputHandler
{
    public override void OnEnter()
    {
        followPlayer.IsTrasformed = false;
        controller.EnableOnlyCollider(null);
        Debug.Log("Neutral Mode");
        _animator.SetTrigger("Neutral");
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
