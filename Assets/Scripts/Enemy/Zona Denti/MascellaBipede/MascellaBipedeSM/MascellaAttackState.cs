using UnityEngine;

public class MascellaAttackState : MascellaState
{
    public override void OnEnter()
    {
        Debug.Log("Mascella is attacking!");
        mascellaController.isMascellaAttacking = true;
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
        mascellaController.isMascellaAttacking = false;
    }
}