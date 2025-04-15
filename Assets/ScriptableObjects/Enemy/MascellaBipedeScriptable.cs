using UnityEngine;

[CreateAssetMenu(fileName = "MascellaBipedeScriptable", menuName = "Scriptable Objects/MascellaBipedeScriptable")]
public class MascellaBipedeScriptable : ScriptableObject
{
    public float MascellaHealth;
    public float attackDamage;
    public RaycastHit2D sightRange;
}
