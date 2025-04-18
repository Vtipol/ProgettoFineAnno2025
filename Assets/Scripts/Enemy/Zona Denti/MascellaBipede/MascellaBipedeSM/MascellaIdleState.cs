using UnityEngine;

public class MascellaIdleState : MascellaState
{
    private MascellaIdle Idle;
    public override void OnEnter()
    {
        Idle = GetComponentInParent<MascellaIdle>();
        Debug.Log("Mascella is in Idle");
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
