public class AirState : ControlState
{
    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (isOnGround) return;
        CheckVelocityDirection();
    }

    private void CheckVelocityDirection()
    {
        float velocityY = stateController.rigidBody.velocity.y;

        if (velocityY < 0)
        {
            stateController.ChangeState(stateController.fallState);
        }
    }
}
