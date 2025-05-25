using UnityEngine;

public class MascellaStateController : MonoBehaviour
{ 
    private MascellaStateMachine mascellaMachine;
    private Player player;
    public bool isMascellaWalking;
    public bool Spotted;
    public bool isMascellaChasing;
    public bool isMascellaAttacking;
    public bool isMascellaCrashed;
    public bool isMascellaGetUp;
    public bool isMascellaStunned;
    public MascellaBipedeScriptable mascellaStats;
    public MascellaIdle mascellaIdle;
    public MascellaChase mascellaChase;
    public MascellaAttack mascellaAttack;
    public MascellaVulnerable mascellaVulnerable;
    public MascellaIdleState mascellaIdleState;
    public MascellaChaseState mascellaChaseState;
    public MascellaAttackState mascellaAttackState;
    public MascellaCrashedState mascellaCrashedState;
    public MascellaTiredState mascellaTiredState;
    public MascellaStunnedState mascellaStunnedState;
    public MascellaPerception mascellaPerception;
    public GroundChecker groundChecker;
    public Animator mascellaAnimator;
    public AudioSource mascellaAudioSource;
    public AudioClip mascellaDeath;
    public Damageble mascellaDamageble;

    private void Start()
    {
        mascellaMachine = new MascellaStateMachine();
        player = FindAnyObjectByType<Player>();
        mascellaIdleState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker, mascellaPerception, mascellaStats, mascellaAttack, mascellaVulnerable, mascellaAnimator, player);
        mascellaChaseState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker, mascellaPerception, mascellaStats, mascellaAttack, mascellaVulnerable, mascellaAnimator, player);
        mascellaAttackState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker, mascellaPerception, mascellaStats, mascellaAttack, mascellaVulnerable, mascellaAnimator, player);
        mascellaCrashedState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker, mascellaPerception, mascellaStats, mascellaAttack, mascellaVulnerable, mascellaAnimator, player);
        mascellaTiredState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker, mascellaPerception, mascellaStats, mascellaAttack, mascellaVulnerable, mascellaAnimator, player);
        mascellaStunnedState.Initialize(mascellaMachine, this, mascellaIdle, mascellaChase, groundChecker, mascellaPerception, mascellaStats, mascellaAttack, mascellaVulnerable, mascellaAnimator, player);

        mascellaMachine.EnterState(mascellaIdleState); 
    }
    public void MascellaSwitchState(MascellaState newState)
    {
        mascellaMachine.EnterState(newState);
    }
    private void Awake()
    {
        mascellaIdleState = GetComponentInChildren<MascellaIdleState>();
        mascellaChaseState = GetComponentInChildren<MascellaChaseState>();
        mascellaAttackState = GetComponentInChildren<MascellaAttackState>();
        mascellaCrashedState = GetComponentInChildren<MascellaCrashedState>();
        mascellaTiredState = GetComponentInChildren<MascellaTiredState>();
        mascellaStunnedState = GetComponentInChildren<MascellaStunnedState>();
    }
    private void Update()
    {
        mascellaMachine.UpdateState();
        UpdateAnimation();
        mascellaVulnerable.Die();
    }

    private void UpdateAnimation()
    {
        mascellaAnimator.SetBool("isWalking", isMascellaWalking);
        mascellaAnimator.SetBool("Spotted", Spotted);
        mascellaAnimator.SetBool("isChasing", isMascellaChasing);
        mascellaAnimator.SetBool("isAttacking", isMascellaAttacking);
        mascellaAnimator.SetBool("isCrashed", isMascellaCrashed);
        mascellaAnimator.SetBool("isStunned", isMascellaStunned);
        mascellaAnimator.SetBool("isGetUp", isMascellaGetUp);
    }
    
}
