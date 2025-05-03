using UnityEngine;

public class MascellaCrashedState : MascellaState
{
    private float timer;

    public override void OnEnter()
    {
        timer = 0f;
        mascellaController.isMascellaCrashed = true;
        mascellaVulnerable.ActivateWeakSpot(true);
    }

    public override void OnUpdate()
    {
     if (mascellaVulnerable.receivedHit)
        {
            mascellaController.MascellaSwitchState(mascellaController.mascellaStunnedState);
        }
        timer += Time.deltaTime;
        if (timer >= mascellaStats.vulnerableTime && !mascellaVulnerable.receivedHit)
        {
            mascellaController.MascellaSwitchState(mascellaController.mascellaIdleState);
            mascellaVulnerable.ActivateWeakSpot(false);
        }
    }

    public override void OnExit()
    {
        mascellaController.isMascellaCrashed = false;
    }
}