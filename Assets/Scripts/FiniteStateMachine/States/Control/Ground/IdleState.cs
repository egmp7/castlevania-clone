using UnityEngine;

public class IdleState : GroundState
{
    protected override void OnEnter()
    {
        base.OnEnter();
        stateController.rigidBody.velocity =
            new Vector2(
                0,
                stateController.rigidBody.velocity.y);
    }
}