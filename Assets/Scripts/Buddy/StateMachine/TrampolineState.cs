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
