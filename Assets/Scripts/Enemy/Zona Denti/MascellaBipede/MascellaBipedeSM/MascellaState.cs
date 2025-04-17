using UnityEngine;

public abstract class MascellaState : MonoBehaviour
{
    protected MascellaStateMachine mascellaMachine;
    protected MascellaStateController mascellaController;
    
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }

    public void Initialize(MascellaStateMachine mascellaMachine, MascellaStateController mascellaController)
    {
        this.mascellaMachine = mascellaMachine;
        this.mascellaController = mascellaController;
    }
}
