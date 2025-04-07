using UnityEngine;
using System;
public class BuddyStateController : MonoBehaviour
{
    public NeutralState neutralState;
    public SoapState soapState;
    public GumState gumState;
    public TrampolineState trampolineState;
    public PickedUp pickedUp;

    private BuddyStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new BuddyStateMachine();
    }

    private void Start()
    {
        neutralState.Initialize(this, stateMachine, GetComponent<FollowPlayer>());
        gumState.Initialize(this, stateMachine, GetComponent<FollowPlayer>());
        soapState.Initialize(this, stateMachine, GetComponent<FollowPlayer>());
        trampolineState.Initialize(this, stateMachine, GetComponent<FollowPlayer>());

        stateMachine.EnterState(neutralState);
    }
    public void SwitchState(BuddyState newState)
    {
        if (pickedUp.IsPickedUp)
        {
            Debug.Log("Buddy is picked up");
            return;
        }
        stateMachine.EnterState(newState);
    }
    private void Update()
    {
        stateMachine.UpdateState();
    }

}
