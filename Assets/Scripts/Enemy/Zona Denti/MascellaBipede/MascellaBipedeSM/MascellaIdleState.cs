using UnityEngine;

public class MascellaIdleState : MascellaState
{
    private MascellaIdle Idle;
    public override void OnEnter()
    {
        Idle = GetComponentInParent<MascellaIdle>();
    }
    public override void OnUpdate()
    {
        if (Idle.PlayerSighted)
        {
            mascellaController.MascellaSwitchState(mascellaController.mascellaChaseState);
        }
    }
    public override void OnExit()
    {
        
    }
}
