using UnityEngine;

public class MascellaAttackState : MascellaState
{
    public override void OnEnter()
    {
        Debug.Log("Mascella is attacking!");
        mascellaAttack.ResetAttackFlags();
        mascellaAttack.PerformLunge();
    }
    public override void OnUpdate()
    {
        if (mascellaAttack.HasMissed)
        {
            mascellaController.MascellaSwitchState(mascellaController.mascellaCrashedState);
        }
    }
    public override void OnExit()
    {

    }
}
