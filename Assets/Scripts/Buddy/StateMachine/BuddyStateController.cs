using UnityEngine;
using UnityEngine.InputSystem;

public class BuddyStateController : MonoBehaviour
{
    public NeutralState neutralState;
    public SoapState soapState;
    public GumState gumState;
    public TrampolineState trampolineState;
    public PickedUp pickedUp;
    public Animator _animator;
    public Soap soap;

    private BuddyStateMachine stateMachine;

    [SerializeField] public CircleCollider2D circleCollider;
    [SerializeField] public BoxCollider2D boxCollider;
    [SerializeField] public CapsuleCollider2D capsuleCollider;
    public InputAction transformLeftAction;
    public InputAction transformRightAction;

    private void OnEnable()
    {
        transformLeftAction.Enable();
        transformRightAction.Enable();
    }

    private void OnDisable()
    {
        transformLeftAction.Disable();
        transformRightAction.Disable();
    }
    private void Awake()
    {
        stateMachine = new BuddyStateMachine();
        //circleCollider = GetComponent<CircleCollider2D>();
        //boxCollider = GetComponent<BoxCollider2D>();
        //capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        neutralState.Initialize(this, stateMachine, GetComponent<FollowPlayer>(), GetComponent<PickedUp>(), _animator, soap);
        gumState.Initialize(this, stateMachine, GetComponent<FollowPlayer>(), GetComponent<PickedUp>(), _animator, soap);
        soapState.Initialize(this, stateMachine, GetComponent<FollowPlayer>(), GetComponent<PickedUp>(), _animator, soap);
        trampolineState.Initialize(this, stateMachine, GetComponent<FollowPlayer>(), GetComponent<PickedUp>(), _animator, soap);


        stateMachine.EnterState(neutralState);
    }

    public void SwitchState(BuddyState newState)
    {
        stateMachine.EnterState(newState);
    }

    public void EnableOnlyCollider(Collider2D active)
    {
        circleCollider.enabled = active == circleCollider;
        boxCollider.enabled = active == boxCollider;
        capsuleCollider.enabled = active == capsuleCollider;
    }
    private void Update()
    {
        HandleInput();
        stateMachine.UpdateState();
    }

    private void HandleInput()
    {
        if (!pickedUp.IsPickedUp)
        {
            if (InputManager.TransformRightWasPressed)
            {
                stateMachine.HandleInput("E");
            }

            if (InputManager.TransformLeftWasPressed)
            {
                stateMachine.HandleInput("Q");
            }
        }
    }
}
