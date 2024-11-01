public class FallState : AirState
{
    protected override void OnEnter()
    {
        base.OnEnter();
        input.animator.Play("Fall");
    }
}
