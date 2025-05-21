using UnityEngine;

public class MascellaStunnedState : MascellaState
{
    private CapsuleCollider2D Body;
    [SerializeField] private CircleCollider2D Precausion;
    public override void OnEnter()
    {
        Debug.Log("Mascella is stunned");
        mascellaController.isMascellaCrashed = false;
        mascellaController.isMascellaStunned = true;
        Body = GetComponentInParent<CapsuleCollider2D>();
        Body.enabled = false;
        Precausion.enabled = true;
    }

    public override void OnUpdate()
    {
      
    }

    public override void OnExit()
    {

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
