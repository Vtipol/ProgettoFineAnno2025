using UnityEngine;

public class DentoneStateMachine : MonoBehaviour
{
    private DentoneState currentState;

    public void EnterState(DentoneState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter();
        }
    }

    public void UpdateState()
    {
        currentState?.OnUpdate();
    }


    public void ExitState()
    {
        currentState?.OnExit();
        currentState = null;
    }
}
