using UnityEngine;

public class SoapState : BuddyState, IInputHandler
{
    public bool IsSoapy = false;
    public override void OnEnter()
    {
        Debug.Log("Entered SoapState");
        controller.EnableOnlyCollider(controller.boxCollider);
        followPlayer.IsTrasformed = true;
        transform.parent.parent.gameObject.layer = LayerMask.NameToLayer("Trasformation");
        
        IsSoapy = true;
        if (IsSoapy) {Debug.Log("IsSoapy");}
        
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
        IsSoapy = false ;
        transform.parent.parent.gameObject.layer = LayerMask.NameToLayer("Buddy");
    }
}
