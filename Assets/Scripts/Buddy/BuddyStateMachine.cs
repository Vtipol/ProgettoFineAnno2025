using UnityEngine;

public class BuddyStateMachine : MonoBehaviour
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
        if (currentState != null)
        {
            currentState.OnExit();
            currentState = null;
        }
    }
}
