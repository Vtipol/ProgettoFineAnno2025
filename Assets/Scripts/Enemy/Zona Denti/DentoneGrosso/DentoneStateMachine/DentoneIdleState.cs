using UnityEngine;

public class DentoneIdleState : DentoneState
{
    public override void OnEnter()
    {

    }
    public override void OnUpdate()
    {
        _dentoneIdle.Wander();
    }
    public override void OnExit()
    {

    }
}
