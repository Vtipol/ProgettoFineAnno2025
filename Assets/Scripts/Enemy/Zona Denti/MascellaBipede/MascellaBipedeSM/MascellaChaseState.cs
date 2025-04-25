using UnityEditor;
using UnityEngine;

public class MascellaChaseState : MascellaState
{
    private float lostPlayerTimer = 0f;
    private float lostPlayerThreshold = 2f;
    public override void OnEnter()
    {
        Debug.Log("Mascella is chasing");
    }

    public override void OnUpdate()
    {
        if (mascellaChase.PlayerInRange)
        {
            mascellaChase.StopChasing();
            mascellaController.MascellaSwitchState(mascellaController.mascellaAttackState);
            return;
        }
        if (!groundChecker.IsGroundAhead())
        {
            mascellaChase.StopChasing();
            mascellaController.MascellaSwitchState(mascellaController.mascellaIdleState);
            return;
        }

        if (mascellaChase.IsPlayerTooFar())
        {
            lostPlayerTimer += Time.deltaTime;
            if (lostPlayerTimer >= lostPlayerThreshold)
            {
                mascellaChase.StopChasing();
                mascellaController.MascellaSwitchState(mascellaController.mascellaIdleState);
                return;
            }
        }
        else
        {
            lostPlayerTimer = 0f;
        }

        mascellaChase.ChasePlayer();
    }

    public override void OnExit()
    {
    }
}