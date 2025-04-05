using UnityEngine;

public class GumState : BuddyState
{
    public override void OnEnter()
    {
        Debug.Log("Entered GumState");
    }
    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.SwitchState(controller.soapState);
        }
    }
    public override void OnExit()
    {
      
    }
}
