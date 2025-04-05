using UnityEngine;

public class TrampolineState : BuddyState
{
    public override void OnEnter()
    {
        Debug.Log("Entered TrampolineState");
    }
    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.SwitchState(controller.neutralState);
        }
    }
    public override void OnExit()
    {
        
    }
}
