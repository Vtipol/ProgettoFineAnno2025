using UnityEngine;

public class DentoneStateController : MonoBehaviour
{
    private DentoneStateMachine _dentoneStateMachine;
    public DentoneIdleState _dentoneIdleState;
    public DentoneIdle _dentoneIdle;

    private void Start()
    {
        _dentoneStateMachine = new DentoneStateMachine();
        _dentoneIdleState.Initialiaze(_dentoneIdle);
        _dentoneStateMachine.EnterState(_dentoneIdleState);
    }

    private void Awake()
    {
        GetComponentInChildren<DentoneIdleState>();
    }
    public void DentoneSwitchState(DentoneState NewState)
    {
        _dentoneStateMachine.EnterState(NewState);
    }
    public void Update()
    {
        _dentoneStateMachine.UpdateState();
    }

}
