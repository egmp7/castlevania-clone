namespace Player.StateManagement
{

    public class FallState : AirState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            input.Animator.Play("Fall");
        }
    }
}