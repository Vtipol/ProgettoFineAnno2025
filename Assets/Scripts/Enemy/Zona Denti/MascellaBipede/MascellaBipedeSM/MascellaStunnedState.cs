using UnityEngine;
using System.Collections;

public class MascellaStunnedState : MascellaState
{
    

    public override void OnEnter()
    {
        Debug.Log("Mascella is stunned");
        mascellaController.isMascellaStunned = true;
        mascellaController.isMascellaCrashed = false;
    }

    public override void OnUpdate()
    {
      
    }

    public override void OnExit()
    {
        mascellaVulnerable.receivedHit = false;
    }
    //private IEnumerator HandleStun()
    //{
    //    yield return new WaitForSeconds(mascellaStats.stunnedTime);
    //    mascellaVulnerable.ActivateWeakSpot(false);
    //    mascellaController.isMascellaStunned = false;
    //    mascellaController.isMascellaGetUp = true;
    //    yield return new WaitForSeconds(1f);
    //    mascellaController.isMascellaGetUp = false;
    //    mascellaController.MascellaSwitchState(mascellaController.mascellaIdleState);
    //}

}
