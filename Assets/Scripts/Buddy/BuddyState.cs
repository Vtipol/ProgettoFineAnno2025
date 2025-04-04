using UnityEngine;

public abstract class BuddyState : MonoBehaviour
{
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}
