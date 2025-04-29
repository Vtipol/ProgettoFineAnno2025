using UnityEngine;

public abstract class DentoneState : MonoBehaviour
{
    protected DentoneIdle _dentoneIdle;
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }

    public void Initialiaze(DentoneIdle dentoneIdle)
    {
        this._dentoneIdle = dentoneIdle;
    }
}
