using UnityEngine;
using System.Collections;

public class MascellaStunnedState : MascellaState
{
    private float timer = 0f;

    public override void OnEnter()
    {
        Debug.Log("Mascella is stunned");
        mascellaController.isMascellaStunned = true;
        mascellaController.StartCoroutine(HandleStun());
    }

    public override void OnUpdate()
    {
      
    }

    public override void OnExit()
    {
        mascellaVulnerable.receivedHit = false;
        mascellaVulnerable.ActivateWeakSpot(false);
        mascellaController.isMascellaStunned = false;
    }
    private IEnumerator HandleStun()
    {
        yield return new WaitForSeconds(mascellaStats.stunnedTime);
        mascellaController.isMascellaGetUp = true;
        yield return new WaitForSeconds(0.05f);
        mascellaController.isMascellaGetUp = false;
        yield return new WaitForSeconds(0.05f);
        mascellaController.MascellaSwitchState(mascellaController.mascellaIdleState);
    }

}
