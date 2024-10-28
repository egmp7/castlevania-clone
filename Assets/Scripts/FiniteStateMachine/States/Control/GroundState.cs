
using UnityEngine;

public abstract class GroundState : ControlState
{
    protected override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("OnEnter GroundState");
        //sc.ChangeState(sc.idleState);
    }
}