namespace Player.StateManagement
{

    public class PlayerKick02State : AttackState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            input.animator.Play("Kick02");
        }
    }
}