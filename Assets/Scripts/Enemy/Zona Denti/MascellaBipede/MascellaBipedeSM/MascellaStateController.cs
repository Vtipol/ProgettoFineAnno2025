using UnityEngine;

public class MascellaStateController : MonoBehaviour
{ 
    private MascellaStateMachine mascellaMachine;
    public  MascellaIdle mascellaIdle;
    public MascellaChase mascellaChase;
    public MascellaIdleState mascellaIdleState;
    public MascellaChaseState mascellaChaseState;
    public MascellaAttackState mascellaAttackState;
    public MascellaCrashedState mascellaCrashedState;
    public MascellaTiredState mascellaTiredState;
    public GroundChecker groundChecker;

    private void Start()
    {
        mascellaMachine = new MascellaStateMachine();
        mascellaIdleState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker);
        mascellaChaseState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker);
        mascellaAttackState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker);
        mascellaCrashedState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker);
        mascellaTiredState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker);

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
