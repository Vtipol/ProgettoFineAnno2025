using UnityEngine;

public class MascellaIdleState : MascellaState
{
    private float searchDelayTimer = 0.00001f;

    private float chaseCooldownTimer = 0f;
    private const float chaseCooldownDuration = 0.000001f; 

    public override void OnEnter()
    {
        Debug.Log("Mascella is in Idle");
        mascellaAttack.rb.gravityScale = mascellaStats.defaultGravityScale;
        searchDelayTimer = 0.00001f;
        chaseCooldownTimer = chaseCooldownDuration;
        if (mascellaAnimator == null)
        {
            Debug.LogError("Animator not assigned on MascellaStateController!");
        }
    }

    public override void OnUpdate()
    {
        mascellaController.isMascellaWalking = true;
        mascellaIdle.Wander();
        if (searchDelayTimer > 0f)
        {
            searchDelayTimer -= Time.deltaTime;
            return;
        }

        if (chaseCooldownTimer > 0f)
        {
            chaseCooldownTimer -= Time.deltaTime;
            return;
        }

        if (mascellaPerception.CanSeePlayer())
        {
            mascellaController.MascellaSwitchState(mascellaController.mascellaChaseState);
        }
    }

    public override void OnExit()
    {
        mascellaController.isMascellaWalking = false;
    }
}
