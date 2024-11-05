namespace Player.StateManagement
{

    public class PlayerPunch02State : AttackState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            input.animator.Play("Punch02");
        }
    }
}
