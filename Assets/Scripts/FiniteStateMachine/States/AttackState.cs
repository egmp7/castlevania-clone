public class AttackState : State
{
    protected override void OnEnter()
    {
        base.OnEnter();
        input.animator.Play("Punch01");
    }
}
