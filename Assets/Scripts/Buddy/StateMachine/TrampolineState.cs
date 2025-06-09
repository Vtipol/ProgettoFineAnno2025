using UnityEngine;

public class TrampolineState : BuddyState, IInputHandler
{
    public override void OnEnter()
    {
        controller.EnableOnlyCollider(controller.capsuleCollider);
        followPlayer.IsTrasformed = true;
        Debug.Log("Trampoline Mode");
        _animator.SetTrigger("Trampoline");
    }
    public override void OnUpdate()
    {
        if (!followPlayer.targetInView)
        {
            TeleportToPlayer();
        }
        if (!controller.capsuleCollider.enabled)
        {
            controller.capsuleCollider.enabled = true;
        }
    }
    public void OnInput(string input)
    {
        
            switch (input)
            {
                case "R":
                    controller.SwitchState(controller.neutralState);
                    break;
                case "Q":
                    controller.SwitchState(controller.gumState);
                    break;
            }
        
    }
    public override void OnExit()
    {
        controller.EnableOnlyCollider(null);
    }
}
