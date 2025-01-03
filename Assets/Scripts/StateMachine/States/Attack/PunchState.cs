using GameProject.Utilities;
using UnityEngine;

namespace Player.StateManagement
{

    public class PunchState : ComboState
    {
        private static CooldownTimer cooldownTimer = new (0);

        protected override void OnEnter()
        {
            base.OnEnter();
            MaxCombo = 3;
            ComboResetTime = input.punchComboResetTime;
            _attackRadius = 0.25f;
        }

        protected override void OnAttack()
        {
            if (!cooldownTimer.IsCooldownComplete()) return;

            base.OnAttack();
            
            if (CurrentCombo == 1 )
            {
                cooldownTimer = new CooldownTimer(300);
                _damageValue = 20;
                _localOfsset = new Vector2(0.3f, -0.3f);
                input.Animator.Play("Punch01");
            }

            if (CurrentCombo == 2)
            {
                cooldownTimer = new CooldownTimer(150);
                _damageValue = 10;
                _localOfsset = new Vector2(0.3f, 0.5f);
                input.Animator.Play("Punch02");
            }

            if (CurrentCombo == 3)
            {
                cooldownTimer = new CooldownTimer(220);
                _damageValue = 33;
                _localOfsset = new Vector2(0.1f, 1f);
                input.Animator.Play("Punch03");
            }
        }

    }
}
