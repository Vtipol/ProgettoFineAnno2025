using UnityEngine;

public class TrampolineState : BuddyState
{
    BuddyStateController controller;
    BuddyStateMachine machineState;
    FollowPlayer followPlayer;
    public override void OnEnter()
    {
        Debug.Log("Entered TrampolineState");
    }
    public override void OnUpdate()
    {
        machineState.UpdateState();
    }
    public override void OnExit()
    {
        if (Input.GetKey("Space"))
        controller.SwitchState(new SoapState());
    }
}
