using UnityEngine;

public class WalkState : GroundState
{
    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        InputSystemController.MoveState currentMoveState = input.inputSystemController.currentMoveState;

        int facing = 0;

        if (currentMoveState == InputSystemController.MoveState.Right)
        {
            facing = 1;
        }
        if (currentMoveState == InputSystemController.MoveState.Left)
        {
            facing = -1;
        }

        // Lerp for smooth acceleration
        float currentSpeed = Mathf.Lerp(
            input.rigidBody.velocity.x,
            facing * input.walkSpeed,
            input.accelerationRate * Time.fixedDeltaTime);

        // Preserve the vertical velocity and update horizontal speed
        input.rigidBody.velocity = 
            new Vector2(
                currentSpeed, 
                input.rigidBody.velocity.y);
    }
}
