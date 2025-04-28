using UnityEngine;

[CreateAssetMenu(fileName = "MascellaBipedeScriptable", menuName = "Scriptable Objects/MascellaBipedeScriptable")]
public class MascellaBipedeScriptable : ScriptableObject
{
    public float sightRange = 15f;
    public float chaseRange = 25f;
    public float walkSpeed = 5f;
    public float chaseSpeed = 10f;
    public float acceleration = 5f;
    public float tiredTime = 5f;
    public float vulnerableDuration = 3f;
    public float attackDamage = 10f;
    public float jumpForce = 3f;
    public float runTimer = 0f;
    public float forgetDelay = 10;
    public float wanderTimer = 0f;
    public float wanderDuration = 2f;
    public float currentWanderDuration = 1f;
    public float mascellaLungeForce = 2f;
    public float mascellaLungeHorizontalForce = 3f;
    public float mascellaLungeVerticalForce = 5f;
    public int wanderDirection = 1;
    public float pauseDuration = 1f;
    public float pauseTimer = 0f;
    public bool isPaused = false;
    public bool isCrashed = false;
}
