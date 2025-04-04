using UnityEngine;

public class NeutralState : BuddyState
{
    FollowPlayer followPlayer;
    public override void OnEnter()
    {
        followPlayer.IsTrasformed = false;
    }
    public override void OnUpdate()
    {
        
    }
    public override void OnExit()
    {
        
    }
}
