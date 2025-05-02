using UnityEngine;

[CreateAssetMenu(fileName = "DentoneGrossoScriptable", menuName = "Scriptable Objects/DentoneGrossoScriptable")]
public class DentoneGrossoScriptable : ScriptableObject
{
    [Header("Walk")]
    public float walkSpeed = 5f;

    [Header("Wander")]
    public float wanderTimer = 0f;
    public float wanderDuration = 2f;

    [Header("Sight")]
    public float sightRange = 15f;
    public float forgetDelay = 10;

    [Header("Paused")]
    public float pauseDuration = 1f;
    public float pauseTimer = 0f;
    public float currentWanderDuration = 1f;
    public int wanderDirection = 1;
    public bool isPaused = false;

    /*public float chaseRange = 25f;
    public float chaseSpeed = 10f;
    public float acceleration = 5f;
    public float tiredTime = 5f;
    public float vulnerableDuration = 3f;
    public float jumpForce = 3f;
    public float runTimer = 0f;
    public float vulnerableTime = 5f;*/
}