using UnityEngine;

public class BuddyStateMachine 
{
    private BuddyState currentState;

    public void EnterState(BuddyState newState)
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
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

    public void ExitState()
    {
            currentState.OnExit();
            currentState = null;
    }
}
