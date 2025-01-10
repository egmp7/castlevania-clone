using egmp7.Game.Combat;
using GameProject.Utilities;
using UnityEngine;

namespace Player.StateManagement
{

    public class KickState : ComboState
    {
        private static CooldownTimer cooldownTimer = new(0);

        protected override void OnEnter()
        {
            base.OnEnter();
            MaxCombo = 3;
            ComboResetTime = input.kickComboResetTime;
            _attackRadius = 0.35f;
        }

        protected override void OnAttack()
        {
            if (!cooldownTimer.IsCooldownComplete()) return;

            base.OnAttack();

            if (CurrentCombo == 1)
            {
                cooldownTimer = new CooldownTimer(300);
                _attackAmount = 70;
                _attackOffset = new Vector2(0.3f, 0.4f);
                input.Animator.Play("Kick01");
            }

            if (CurrentCombo == 2)
            {
                cooldownTimer = new CooldownTimer(280);
                _attackAmount = 40;
                _attackOffset = new Vector2(0.3f, -0.3f);
                input.Animator.Play("Kick02");
            }
            if (CurrentCombo == 3)
            {
                cooldownTimer = new CooldownTimer(600);
                _attackAmount = 100;
                _attackOffset = new Vector2(0.3f, 0.1f);
                input.Animator.Play("Kick03");
            }

            // set Attack
            Attack attack = new(
                _attackOffset,
                _attackRadius,
                _attackAmount,
                _attackFrom,
                _attackTo);

            input.damageProcessor.SetFromAttack(attack);
        }
    }
}
