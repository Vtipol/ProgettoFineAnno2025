using UnityEngine;

public class SoapState : BuddyState
{
    public override void OnEnter()
    {
        Debug.Log("Entered SoapState");
    }
    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.SwitchState(controller.trampolineState);
        }
    }
    public override void OnExit()
    {
     
    }
}
