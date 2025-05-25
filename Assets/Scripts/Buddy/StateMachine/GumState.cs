using UnityEngine;
using UnityEngine.InputSystem;

public class GumState : BuddyState, IInputHandler
{
    public override void OnEnter()
    {
        controller.EnableOnlyCollider(controller.circleCollider);
        followPlayer.IsTrasformed = true;
        controller.gum.numberOfGumJumps = 0;
        Debug.Log("Gum Mode");
        _animator.SetTrigger("Gum");
    }
    public override void OnUpdate()
    {
        if (!followPlayer.targetInView)
        {
            TeleportToPlayer();
        }
        controller.gum.CheckStuck();
        controller.gum.GumStuck();
        controller.gum.PlayerGumJump();
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
        controller.gum.IsStuck = false;
        controller.gum.GumStuck();
        controller.EnableOnlyCollider(null);
    }
}
