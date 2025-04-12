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
        currentState?.OnUpdate();
    }

    public void HandleInput(string input)
    {
        if (currentState is IInputHandler inputHandler)
        {
            inputHandler.OnInput(input);
        }
    }

    public void ExitState()
    {
        currentState?.OnExit();
        currentState = null;
    }
}