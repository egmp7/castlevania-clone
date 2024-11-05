public class PlayerPunch01State : AttackState
{
    protected override void OnEnter()
    {
        base.OnEnter();
        input.animator.Play("Punch01");
    }
}