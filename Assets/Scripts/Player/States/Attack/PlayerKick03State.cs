namespace Player.StateManagement
{
    public class PlayerKick03State : AttackState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            input.animator.Play("Kick03");
        }
    }
}
