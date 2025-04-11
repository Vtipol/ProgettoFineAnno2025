using UnityEngine;

public class BuddyStateController : MonoBehaviour
{
    public NeutralState neutralState;
    public SoapState soapState;
    public GumState gumState;
    public TrampolineState trampolineState;
    public PickedUp pickedUp;

    private BuddyStateMachine stateMachine;

    [SerializeField] public CircleCollider2D circleCollider;
    [SerializeField] public BoxCollider2D boxCollider;
    [SerializeField] public CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        stateMachine = new BuddyStateMachine();
        //circleCollider = GetComponent<CircleCollider2D>();
        //boxCollider = GetComponent<BoxCollider2D>();
        //capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        neutralState.Initialize(this, stateMachine, GetComponent<FollowPlayer>(), GetComponent<PickedUp>());
        gumState.Initialize(this, stateMachine, GetComponent<FollowPlayer>(), GetComponent<PickedUp>());
        soapState.Initialize(this, stateMachine, GetComponent<FollowPlayer>(), GetComponent<PickedUp>());
        trampolineState.Initialize(this, stateMachine, GetComponent<FollowPlayer>(), GetComponent<PickedUp>());


        stateMachine.EnterState(neutralState);
    }

    public void SwitchState(BuddyState newState)
    {
        stateMachine.EnterState(newState);
    }

    public void EnableOnlyCollider(Collider2D active)
    {
        Debug.Log("changed Collider");
        circleCollider.enabled = active == circleCollider;
        boxCollider.enabled = active == boxCollider;
        capsuleCollider.enabled = active == capsuleCollider;
    }

    private void Update()
    {
        stateMachine.UpdateState();
    }
}
