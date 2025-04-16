using UnityEngine;

public class MascellaStateMachine : MonoBehaviour
{
    private MascellaState currentState;

    public void EnterState(MascellaState newState)
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
