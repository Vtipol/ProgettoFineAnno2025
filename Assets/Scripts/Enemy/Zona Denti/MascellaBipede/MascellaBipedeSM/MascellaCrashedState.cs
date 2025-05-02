using UnityEngine;

public class MascellaCrashedState : MascellaState
{
    private float timer;

    public override void OnEnter()
    {
        timer = 0f;
        mascellaController.isMascellaCrashed = true;
    }

    public override void OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= mascellaStats.vulnerableTime)
        {
            mascellaController.MascellaSwitchState(mascellaController.mascellaIdleState);
        }
    }

    public override void OnExit()
    {
        mascellaController.isMascellaCrashed = false;
        mascellaVulnerable.ActivateWeakSpot(false);
    }
}