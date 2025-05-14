using UnityEngine;

public abstract class BuddyState : MonoBehaviour
{
    protected BuddyStateController controller;
    protected BuddyStateMachine machineState;
    protected FollowPlayer followPlayer;
    protected PickedUp pickedUp;
    protected Animator _animator;
    protected Soap soap;

    public void Initialize(BuddyStateController controller, BuddyStateMachine machineState, FollowPlayer followPlayer, PickedUp pickedUp, Animator _animator, Soap soap)
    {
        this.controller = controller;
        this.machineState = machineState;
        this.followPlayer = followPlayer;
        this.pickedUp = pickedUp;
        this._animator = _animator;
        this.soap = soap;
    }
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
    public void TeleportToPlayer()
    {
        Vector3 offset = new Vector3(0f, 0f, 0f);
        controller.transform.position = followPlayer.target.position + offset;
        controller.SwitchState(controller.neutralState);
        controller.EnableOnlyCollider(null);
        Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        _animator.SetTrigger("Teleport");
    }
}
