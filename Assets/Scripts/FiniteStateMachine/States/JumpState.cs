
using UnityEngine;

public class JumpState : AirState
{
    protected override void OnEnter()
    {
        base.OnEnter();
        // jump
        input.rigidBody.velocity =
            new Vector2(
                input.rigidBody.velocity.x,
                input.jumpForce);

        input.animationController.PlayJumpAnimation();
    }
}
