using UnityEngine;
using UnityEngine.InputSystem;

public class GumState : BuddyState, IInputHandler
{
    public override void OnEnter()
    {
        controller.EnableOnlyCollider(controller.circleCollider);
        followPlayer.IsTrasformed = true;
        Debug.Log("Gum Mode");
        _animator.SetTrigger("Gum");
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
                    controller.SwitchState(controller.neutralState);
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
