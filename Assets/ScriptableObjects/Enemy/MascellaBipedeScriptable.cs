using UnityEngine;

[CreateAssetMenu(fileName = "MascellaBipedeScriptable", menuName = "Scriptable Objects/MascellaBipedeScriptable")]
public class MascellaBipedeScriptable : ScriptableObject
{
    public float sightRange = 20f;
    public float speed = 5f;
    public float tiredTime = 5f;
    public float vulnerableDuration = 3f;
    public float jumpForce = 7f;
    public bool isGrounded = true;
    public float runTimer = 0f;
}
