using UnityEngine;
using System.Collections;
public class MascellaCrashedState : MascellaState
{
    private float timer;

    public override void OnEnter()
    {
        timer = 0f;
        mascellaController.isMascellaCrashed = true;
        mascellaController.StartCoroutine(AwaytEnab());
    }

    public override void OnUpdate()
    {
     if (mascellaVulnerable.receivedHit)
        {
            mascellaController.isMascellaCrashed = false;
            mascellaController.MascellaSwitchState(mascellaController.mascellaStunnedState);
        }
        timer += Time.deltaTime;
        if (timer >= mascellaStats.vulnerableTime && !mascellaVulnerable.receivedHit)
        {
            mascellaController.isMascellaCrashed = false;
            mascellaVulnerable.ActivateWeakSpot(false);
            mascellaController.StartCoroutine(GetUp());
        }
    }

    public override void OnExit()
    {

    }
    private IEnumerator GetUp()
    {
        mascellaController.isMascellaGetUp = true;
        yield return new WaitForSeconds(1f);
        mascellaController.isMascellaGetUp = false;
       // yield return new WaitForSeconds(0.5f);
        mascellaController.MascellaSwitchState(mascellaController.mascellaIdleState);
    }
    private IEnumerator AwaytEnab()
    {
        yield return new WaitForSeconds(0.4f);
        mascellaVulnerable.ActivateWeakSpot(true);
    }
}