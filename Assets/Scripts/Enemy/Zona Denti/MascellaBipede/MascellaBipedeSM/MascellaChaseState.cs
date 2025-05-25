using UnityEditor;
using UnityEngine;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class MascellaChaseState : MascellaState
{
    private float lostPlayerTimer = 0f;
    private float lostPlayerThreshold = 0.5f;
    private float groundLostTimer = 0f;
    private float groundLostThreshold = 0.5f;
    public override void OnEnter()
    {
        Debug.Log("Mascella is chasing");
        StartCoroutine(SpottedPlayer());
        mascellaController.isMascellaChasing = true;

    }

    public override void OnUpdate()
    {
        if (mascellaController.Spotted) return;
        if (mascellaChase.PlayerInRange)
        {
            mascellaChase.StopChasing();
            mascellaController.isMascellaChasing = false;
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
                mascellaStats.pauseTimer += -2f;
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
        mascellaChase.CheckTargetElevation();
        if (mascellaChase.CheckTargetElevation()) mascellaController.MascellaSwitchState(mascellaController.mascellaIdleState);

        if (!mascellaChase.isMascellaBackingAway)
        {
            mascellaChase.ChasePlayer();
        }
    }
    private IEnumerator SpottedPlayer()
    {
        mascellaController.Spotted = true;
        yield return new WaitForSeconds(0.5f);
        mascellaController.Spotted = false;

    }
    public override void OnExit()
    {
        mascellaController.isMascellaChasing = false;
    }

}