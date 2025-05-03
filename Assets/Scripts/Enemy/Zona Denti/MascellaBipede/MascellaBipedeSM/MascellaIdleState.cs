using UnityEngine;

public class MascellaIdleState : MascellaState
{
    private float searchDelayTimer = 0.1f;

    private float chaseCooldownTimer = 0f;
    private const float chaseCooldownDuration = 0.1f; 

    public override void OnEnter()
    {
        Debug.Log("Mascella is in Idle");
        mascellaAttack.rb.gravityScale = mascellaStats.defaultGravityScale;
        searchDelayTimer = 0.1f;
        chaseCooldownTimer = chaseCooldownDuration;
        mascellaController.isMascellaWalking = true;
        if (mascellaAnimator == null)
        {
            Debug.LogError("Animator not assigned on MascellaStateController!");
        }
    }

    public override void OnUpdate()
    {
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
