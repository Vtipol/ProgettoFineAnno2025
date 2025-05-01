using UnityEditor;
using UnityEngine;

public class MascellaChaseState : MascellaState
{
    private float lostPlayerTimer = 0f;
    private float lostPlayerThreshold = 0.5f;
    private float groundLostTimer = 0f;
    private float groundLostThreshold = 0.5f;
    public override void OnEnter()
    {
        Debug.Log("Mascella is chasing");
        mascellaController.isMascellaChasing = true;

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
            groundLostTimer += Time.deltaTime;

            if (!mascellaChase.isMascellaBackingAway)
            {
                mascellaChase.Flip();
                mascellaChase.BackAway();
                mascellaChase.isMascellaBackingAway = true;
                mascellaStats.pauseTimer += -1f;
                mascellaController.MascellaSwitchState(mascellaController.mascellaIdleState);
            }

            if (groundLostTimer >= groundLostThreshold)
            {
                mascellaChase.StopChasing();
                mascellaController.MascellaSwitchState(mascellaController.mascellaIdleState);
            }
            return;
        }
        else
        {
            groundLostTimer = 0f;
            mascellaChase.isMascellaBackingAway = false;
        }

        if (!mascellaPerception.CanSeePlayer())
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

        if (!mascellaChase.isMascellaBackingAway)
        {
            mascellaChase.ChasePlayer();
        }
    }

    public override void OnExit()
    {
        mascellaController.isMascellaChasing = false;
    }

}