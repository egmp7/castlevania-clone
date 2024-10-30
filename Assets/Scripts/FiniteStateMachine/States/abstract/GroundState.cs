public abstract class GroundState : State
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
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
}