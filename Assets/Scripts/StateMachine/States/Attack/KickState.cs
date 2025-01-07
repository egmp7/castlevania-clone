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
            input.damageProcessor.attackRadius = 0.35f;
        }

        protected override void OnAttack()
        {
            if (!cooldownTimer.IsCooldownComplete()) return;

            base.OnAttack();

            if (CurrentCombo == 1)
            {
                cooldownTimer = new CooldownTimer(300);
                input.damageProcessor.currentDamage = 70;
                input.damageProcessor.offset = new Vector2(0.3f, 0.4f);
                input.Animator.Play("Kick01");
            }

            if (CurrentCombo == 2)
            {
                cooldownTimer = new CooldownTimer(280);
                input.damageProcessor.currentDamage = 40;
                input.damageProcessor.offset = new Vector2(0.3f, -0.3f);
                input.Animator.Play("Kick02");
            }
            if (CurrentCombo == 3)
            {
                cooldownTimer = new CooldownTimer(600);
                input.damageProcessor.currentDamage = 100;
                input.damageProcessor.offset = new Vector2(0.3f, 0.1f);
                input.Animator.Play("Kick03");
            }
        }
    }
}
