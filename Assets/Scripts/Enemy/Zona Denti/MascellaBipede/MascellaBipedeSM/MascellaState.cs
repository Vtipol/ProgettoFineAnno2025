using UnityEngine;

public abstract class MascellaState : MonoBehaviour
{
    protected MascellaStateMachine mascellaMachine;
    protected MascellaStateController mascellaController;
    protected MascellaBipedeScriptable mascellaStats;
    protected MascellaIdle mascellaIdle;
    protected MascellaChase mascellaChase;
    protected MascellaAttack mascellaAttack;
    protected MascellaVulnerable mascellaVulnerable;
    protected GroundChecker groundChecker;
    protected MascellaPerception mascellaPerception;
    protected Animator mascellaAnimator;
    
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }

    public void Initialize(MascellaStateMachine mascellaMachine, MascellaStateController mascellaController, MascellaIdle mascellaIdle,MascellaChase mascellaChase,
        GroundChecker groundChecker, MascellaPerception mascellaPerception, MascellaBipedeScriptable mascellaStats, MascellaAttack mascellaAttack, MascellaVulnerable mascellaVulnerable, Animator mascellaAnimator)
    {
        this.mascellaMachine = mascellaMachine;
        this.mascellaController = mascellaController;
        this.mascellaIdle = mascellaIdle;
        this.mascellaChase = mascellaChase;
        this.groundChecker = groundChecker;
        this.mascellaPerception = mascellaPerception;
        this.mascellaStats = mascellaStats;
        this.mascellaAttack = mascellaAttack;
        this.mascellaVulnerable = mascellaVulnerable;
        this.mascellaAnimator = mascellaAnimator;
    }
}
