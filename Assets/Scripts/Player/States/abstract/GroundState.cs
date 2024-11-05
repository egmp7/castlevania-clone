using UnityEngine;

public abstract class GroundState : State
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        FlipPlayerBasedOnDirection();
    }

    private void FlipPlayerBasedOnDirection()
    {
        if (input.facing == -1)
        {
            // Moving left, flip the player's x scale to face left
            input.transform.localScale = new Vector3(
                -input.originalScale.x, 
                input.originalScale.y, 
                input.originalScale.z);

        }
        else
        {
            // Moving right, ensure player is facing right
            input.transform.localScale = new Vector3(
                input.originalScale.x, 
                input.originalScale.y, 
                input.originalScale.z);
        }
    }
}