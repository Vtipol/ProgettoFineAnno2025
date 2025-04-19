using UnityEngine;

public class MascellaChaseState : MascellaState
{
    [SerializeField] private MascellaChase mascellaChase;
    private bool isChaseStarted = false;
    public override void OnEnter()
    {
        isChaseStarted = false;
        Debug.Log("Mascella is chasing");
    }
    public override void OnUpdate()
    {
        if (!isChaseStarted)
        {
            mascellaChase.StartChasing();
            isChaseStarted = true;
        }
        if (mascellaChase.PlayerInRange)
        {
            Debug.Log("Player is in attack range");
            mascellaController.MascellaSwitchState(mascellaController.mascellaAttackState);
        }
    }
    public override void OnExit()
    {

    }
}
