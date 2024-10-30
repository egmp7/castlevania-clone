using UnityEngine;

public abstract class GroundState : State
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        FlipPlayerBasedOnDirection();
        SelectState();
    }

    private void SelectState()
    {
        if (!input.isOnGround)
        {
            input.ChangeState(input.fallState);
            return;
        }

        InputSystemController.MoveState currentMoveState = input.inputSystemController.currentMoveState;

        if (currentMoveState == InputSystemController.MoveState.Idle)
        {
            input.ChangeState(input.idleState);
            return;
        }

        if (currentMoveState == InputSystemController.MoveState.Left ||
            currentMoveState == InputSystemController.MoveState.Right)
        {
            if (input.inputSystemController.isDoubleTap)
            {
                input.ChangeState(input.runState);
            }
            else
            {
                input.ChangeState(input.walkState);
            }
            return;
        }

        if (currentMoveState == InputSystemController.MoveState.Up ||
            currentMoveState == InputSystemController.MoveState.UpLeft ||
            currentMoveState == InputSystemController.MoveState.UpRight)
        {
            input.ChangeState(input.jumpState);
            return;
        }
    }

    private void FlipPlayerBasedOnDirection()
    {
        if (input.facing > 0)
        {
            // Moving right, ensure player is facing right
            input.transform.localScale = new Vector3(
                input.originalScale.x, 
                input.originalScale.y, 
                input.originalScale.z);
        }
        else
        {
            // Moving left, flip the player's x scale to face left
            input.transform.localScale = new Vector3(
                -input.originalScale.x, 
                input.originalScale.y, 
                input.originalScale.z);

        }
    }
}