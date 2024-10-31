using UnityEngine;

public class AttackState : State
{
    protected override void OnEnter()
    {
        base.OnEnter();
        input.animationController.PlayAttackAnimation();
    }
}
