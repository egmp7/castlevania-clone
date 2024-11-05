public class PlayerKick01State : AttackState
{
    protected override void OnEnter()
    {
        base.OnEnter();
        input.animator.Play("Kick01");
    }
}