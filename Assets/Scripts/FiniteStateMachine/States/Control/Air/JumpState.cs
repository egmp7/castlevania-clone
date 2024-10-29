
using UnityEngine;

public class JumpState : AirState
{
    protected override void OnEnter()
    {
        base.OnEnter();
        // jump
        stateController.rigidBody.velocity =
            new Vector2(
                stateController.rigidBody.velocity.x,
                stateController.jumpForce);
    }
}
