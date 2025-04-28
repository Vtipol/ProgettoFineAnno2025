using UnityEngine;

public class MascellaCrashedState : MascellaState
{
    public override void OnEnter()
    {

    }
    public override void OnUpdate()
    {
        mascellaController.MascellaSwitchState(mascellaController.mascellaIdleState);
    }
    public override void OnExit()
    {

    }
}
