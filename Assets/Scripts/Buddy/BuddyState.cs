using UnityEngine;

public abstract class BuddyState : MonoBehaviour
{
    protected BuddyStateController controller;
    protected BuddyStateMachine machineState;
    protected FollowPlayer followPlayer;

    public void Initialize(BuddyStateController controller, BuddyStateMachine machineState, FollowPlayer followPlayer)
    {
        this.controller = controller;
        this.machineState = machineState;
        this.followPlayer = followPlayer;
    }

    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}
