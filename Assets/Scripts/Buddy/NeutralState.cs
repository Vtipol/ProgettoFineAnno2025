using UnityEngine;

public class NeutralState : BuddyState
{
    public override void OnEnter()
    {
        Debug.Log("Entered Neutral State");
        followPlayer.IsTrasformed = false;
        followPlayer.Activated();
    }
    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            followPlayer.IsTrasformed = true;
            followPlayer.Deactivate();
            controller.SwitchState(controller.gumState);
        }
    }
    public override void OnExit()
    {
       
    }
}
