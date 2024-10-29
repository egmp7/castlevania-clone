using UnityEngine;

public abstract class ControlState : State
{
    public bool isOnGround;

    protected override void OnUpdate()
    {
        base.OnUpdate();
        CheckGroundStatus();

        if (!isOnGround) return;

        InputController.MoveState currentMoveState = stateController.inputController.currentMoveState;

        if (currentMoveState == InputController.MoveState.Jump)
        {
            stateController.ChangeState(stateController.jumpState);
        }

        else if (currentMoveState == InputController.MoveState.Walk)
        {
            stateController.ChangeState(stateController.walkState);
        }

        else if (currentMoveState == InputController.MoveState.Run)
        {
            stateController.ChangeState(stateController.runState);
        }

        else if (currentMoveState == InputController.MoveState.Idle)
        {
            stateController.ChangeState(stateController.idleState);
        }
    }

    private void CheckGroundStatus()
    {
        Vector2 groundDetectorPosition = stateController.groundDetector.position;
        Vector2 groundDetectorSize = stateController.groundDetectorSize;
        LayerMask groundLayer = stateController.groundLayer;

        RaycastHit2D hit = Physics2D.BoxCast(
            groundDetectorPosition,
            groundDetectorSize,
            0f,
            Vector2.right,
            0f,
            groundLayer);

        isOnGround = hit.collider != null;
    }
}