using UnityEngine;

[CreateAssetMenu(fileName = "MascellaBipedeScriptable", menuName = "Scriptable Objects/MascellaBipedeScriptable")]
public class MascellaBipedeScriptable : ScriptableObject
{
    public float sightRange = 20f;
    public float chaseRange = 25f;
    public float speed = 10f;
    public float tiredTime = 5f;
    public float vulnerableDuration = 3f;
    public float attackDamage = 10f;
    public float jumpForce = 3f;
    public float runTimer = 0f;
}
