public class AirState : State
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        CheckVelocityDirection();

        InputSystemController.MoveState currentMoveState = input.inputSystemController.currentMoveState;

        if (input.isOnGround)
        {
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
        }
    }

    private void CheckVelocityDirection()
    {
        float velocityY = input.rigidBody.velocity.y;

        if (velocityY < 0)
        {
            input.ChangeState(input.fallState);
        }
    }
}
