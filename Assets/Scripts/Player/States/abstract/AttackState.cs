using UnityEngine;

public abstract class AttackState : State
{
    protected override void OnEnter()
    {
        base.OnEnter();

        input.rigidBody.velocity =
            new Vector2(
                0,
                input.rigidBody.velocity.y);
    }
}
