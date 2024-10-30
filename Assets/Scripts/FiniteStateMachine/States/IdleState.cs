using UnityEngine;

public class IdleState : GroundState
{
    protected override void OnEnter()
    {
        base.OnEnter();
        input.rigidBody.velocity =
            new Vector2(
                0,
                input.rigidBody.velocity.y);
    }
}