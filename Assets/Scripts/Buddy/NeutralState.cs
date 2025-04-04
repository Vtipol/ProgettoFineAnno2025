using UnityEngine;

public class NeutralState : BuddyState
{
    BuddyStateController controller;
    BuddyStateMachine machineState;
    FollowPlayer followPlayer;
    public override void OnEnter()
    {
        followPlayer.IsTrasformed = false;
    }
    public override void OnUpdate()
    {
      machineState.UpdateState();
    }
    public override void OnExit()
    {
        if (Input.GetKey("Space"))
        {
            followPlayer.IsTrasformed = true;
            controller.SwitchState(new GumState());
        }
    }
}
