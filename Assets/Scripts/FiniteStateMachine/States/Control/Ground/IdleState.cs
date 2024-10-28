using UnityEngine;

public class IdleState : GroundState
{
    protected override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("OnEnter IdleState");
        //animator.Play("Idle");
    }
}