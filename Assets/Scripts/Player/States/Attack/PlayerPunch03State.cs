public class PlayerPunch03State : AttackState
{
    protected override void OnEnter()
    {
        base.OnEnter();
        input.animator.Play("Punch03");
    }
}
