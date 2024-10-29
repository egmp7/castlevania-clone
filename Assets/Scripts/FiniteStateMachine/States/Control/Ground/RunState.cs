using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : GroundState
{
    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        // Lerp for smooth acceleration
        float currentSpeed = Mathf.Lerp(
            stateController.rigidBody.velocity.x,
            stateController.inputController.moveInput.x * stateController.runSpeed,
            stateController.accelerationRate * Time.fixedDeltaTime);
        // Preserve the vertical velocity and update horizontal speed
        stateController.rigidBody.velocity =
            new Vector2(
                currentSpeed,
                stateController.rigidBody.velocity.y);
    }
}
