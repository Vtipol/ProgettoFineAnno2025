using UnityEngine;

public abstract class MascellaState : MonoBehaviour
{
    protected MascellaStateMachine mascellaMachine;
    protected MascellaStateController mascellaController;
    protected MascellaIdle mascellaIdle;
    protected MascellaChase mascellaChase;
    protected GroundChecker groundChecker;
    protected MascellaPerception mascellaPerception;
    
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }

    public void Initialize(MascellaStateMachine mascellaMachine, MascellaStateController mascellaController, MascellaIdle mascellaIdle, MascellaChase mascellaChase, GroundChecker groundChecker, MascellaPerception mascellaPerception)
    {
        this.mascellaMachine = mascellaMachine;
        this.mascellaController = mascellaController;
        this.mascellaIdle = mascellaIdle;
        this.mascellaChase = mascellaChase;
        this.groundChecker = groundChecker;
        this.mascellaPerception = mascellaPerception;
    }
}
