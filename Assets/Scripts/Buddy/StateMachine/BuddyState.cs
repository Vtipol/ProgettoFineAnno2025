using UnityEngine;

public abstract class BuddyState : MonoBehaviour
{
    protected BuddyStateController controller;
    protected BuddyStateMachine machineState;
    protected FollowPlayer followPlayer;
    protected PickedUp pickedUp;

    public void Initialize(BuddyStateController controller, BuddyStateMachine machineState, FollowPlayer followPlayer, PickedUp pickedUp)
    {
        this.controller = controller;
        this.machineState = machineState;
        this.followPlayer = followPlayer;
        this.pickedUp = pickedUp;
    }

    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }

    public void TeleportToPlayer()
    {
        Vector3 offset = new Vector3(0f, 0f, 0f);
        controller.transform.position = followPlayer.target.position + offset;

        Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Buddy"))
        {
            collision.GetComponent<BuddyState>().TeleportToPlayer();
        }
    }
    */
}
