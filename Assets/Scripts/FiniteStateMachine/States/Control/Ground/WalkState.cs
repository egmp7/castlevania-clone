using UnityEngine;

public class WalkState : GroundState
{
    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        // Lerp for smooth acceleration
        float currentSpeed = Mathf.Lerp(
            stateController.rigidBody.velocity.x,
            stateController.inputController.moveInput.x * stateController.walkSpeed,
            stateController.accelerationRate * Time.fixedDeltaTime);
        // Preserve the vertical velocity and update horizontal speed
        stateController.rigidBody.velocity = 
            new Vector2(
                currentSpeed, 
                stateController.rigidBody.velocity.y);
    }
}
