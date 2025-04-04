using UnityEngine;

public class GumState : BuddyState
{
    BuddyStateController controller;
    BuddyStateMachine machineState;
    FollowPlayer followPlayer;
    public override void OnEnter()
    {
        Debug.Log("Entered GumState");
    }
    public override void OnUpdate()
    {
        machineState.UpdateState();
    }
    public override void OnExit()
    {
        if (Input.GetKey("Space"))
        controller.SwitchState(new TrampolineState());
    }
}
