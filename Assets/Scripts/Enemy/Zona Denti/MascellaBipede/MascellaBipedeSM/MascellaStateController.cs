using UnityEngine;

public class MascellaStateController : MonoBehaviour
{ 
    private MascellaStateMachine mascellaMachine;
    public MascellaBipedeScriptable mascellaStats;
    public MascellaIdle mascellaIdle;
    public MascellaChase mascellaChase;
    public MascellaAttack mascellaAttack;
    public MascellaVulnerable mascellaVulnerable;
    public MascellaIdleState mascellaIdleState;
    public MascellaChaseState mascellaChaseState;
    public MascellaAttackState mascellaAttackState;
    public MascellaCrashedState mascellaCrashedState;
    public MascellaTiredState mascellaTiredState;
    public MascellaPerception mascellaPerception;
    public GroundChecker groundChecker;

    private void Start()
    {
        mascellaMachine = new MascellaStateMachine();
        mascellaIdleState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker, mascellaPerception, mascellaStats, mascellaAttack, mascellaVulnerable);
        mascellaChaseState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker, mascellaPerception, mascellaStats, mascellaAttack, mascellaVulnerable);
        mascellaAttackState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker, mascellaPerception, mascellaStats, mascellaAttack, mascellaVulnerable);
        mascellaCrashedState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker, mascellaPerception, mascellaStats, mascellaAttack, mascellaVulnerable);
        mascellaTiredState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker, mascellaPerception, mascellaStats, mascellaAttack, mascellaVulnerable);

        mascellaMachine.EnterState(mascellaIdleState); 
    }
    public void MascellaSwitchState(MascellaState newState)
    {
        mascellaMachine.EnterState(newState);
    }
    private void Awake()
    {
        mascellaIdleState = GetComponentInChildren<MascellaIdleState>();
        mascellaChaseState = GetComponentInChildren<MascellaChaseState>();
        mascellaAttackState = GetComponentInChildren<MascellaAttackState>();
        mascellaCrashedState = GetComponentInChildren<MascellaCrashedState>();
        mascellaTiredState = GetComponentInChildren<MascellaTiredState>();
    }
    private void Update()
    {
        mascellaMachine.UpdateState();
    }
}
