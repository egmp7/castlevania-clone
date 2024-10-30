using UnityEngine;

public class RunState : GroundState
{
    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        // Lerp for smooth acceleration
        float currentSpeed = Mathf.Lerp(
            input.rigidBody.velocity.x,
            input.facing * input.runSpeed,
            input.accelerationRate * Time.fixedDeltaTime);

        // Preserve the vertical velocity and update horizontal speed
        input.rigidBody.velocity =
            new Vector2(
                currentSpeed,
                input.rigidBody.velocity.y);
    }
}
