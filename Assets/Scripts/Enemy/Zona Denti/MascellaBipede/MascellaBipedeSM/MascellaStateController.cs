using UnityEngine;

public class MascellaStateController : MonoBehaviour
{ 
private MascellaStateMachine mascellaMachine;
    public MascellaIdleState mascellaIdleState;
    public MascellaChaseState mascellaChaseState;
    public MascellaAttackState mascellaAttackState;
    public MascellaCrashedState mascellaCrashedState;
    public MascellaTiredState mascellaTiredState;

    private void Start()
    {
        mascellaMachine = new MascellaStateMachine();
        mascellaIdleState.Initialize(mascellaMachine, this);
        mascellaChaseState.Initialize(mascellaMachine, this);
        mascellaAttackState.Initialize(mascellaMachine, this);
        mascellaCrashedState.Initialize(mascellaMachine, this);
        mascellaTiredState.Initialize(mascellaMachine, this);

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
