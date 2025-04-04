using UnityEngine;
using System;
public class BuddyStateController : MonoBehaviour
{
    public NeutralState neutralState;
    public SoapState soapState;
    public GumState gumState;
    public TrampolineState trampolineState;

    private BuddyStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new BuddyStateMachine();
    }

    private void Start()
    {
        stateMachine.EnterState(neutralState);
    }

    private void Update()
    {
        stateMachine.UpdateState();
    }
}
