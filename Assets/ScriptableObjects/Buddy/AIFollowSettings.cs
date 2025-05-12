using UnityEngine;

[CreateAssetMenu(fileName = "AIFollowSettings", menuName = "Scriptable Objects/AIFollowSettings")]
public class AIFollowSettings : ScriptableObject
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float wallJumpForce = 10f;
    public float fastFallSpeed = 5f;
    public float stopDistance = 1f;
    public float fieldOfViewRadius = 10f;
    public LayerMask groundLayer;
    public LayerMask targetLayer;
    public float groundCheckRadius = 1.2f;
    public float fallingSpeed = 2f;
    public float runSpeedBonus = 8f;
    public float decelDistance = 3f;
}
