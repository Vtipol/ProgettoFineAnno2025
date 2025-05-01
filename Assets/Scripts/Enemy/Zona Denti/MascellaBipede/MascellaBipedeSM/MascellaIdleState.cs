using UnityEngine;

public class MascellaIdleState : MascellaState
{
    private float searchDelayTimer = 0.5f;

    private float chaseCooldownTimer = 0f;
    private const float chaseCooldownDuration = 0.5f; 

    public override void OnEnter()
    {
        Debug.Log("Mascella is in Idle");
        mascellaAttack.rb.gravityScale = mascellaStats.defaultGravityScale;
        searchDelayTimer = 0.5f;
        chaseCooldownTimer = chaseCooldownDuration;
        mascellaController.isMascellaWalking = true;
    }

    public override void OnUpdate()
    {
        mascellaIdle.Wander();
        mascellaController.UpdateAnimation();
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
