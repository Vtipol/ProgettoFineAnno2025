using UnityEngine;

public class MascellaIdleState : MascellaState
{
    public override void OnEnter()
    {
        Debug.Log("Mascella is in Idle");
    }
    public override void OnUpdate()
    {
        mascellaIdle.Wander();
        mascellaIdle.SearchPlayer();

        if (mascellaIdle.PlayerSighted)
        {
            mascellaController.MascellaSwitchState(mascellaController.mascellaChaseState);
        }
    }
    public override void OnExit()
    {
        
    }
}
