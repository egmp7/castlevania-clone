using GameProject.Utilities;

namespace Player.StateManagement
{

    public class KickState : ComboState
    {
        private static CooldownTimer cooldownTimer = new(0);

        protected override void OnEnter()
        {
            base.OnEnter();
            MaxCombo = 3;
            ComboResetTime = input.KickComboResetTime;
        }

        protected override void OnAttack()
        {
            if (!cooldownTimer.IsCooldownComplete()) return;

            base.OnAttack();

            if (CurrentCombo == 1)
            {
                cooldownTimer = new CooldownTimer(300);
                input.animator.Play("Kick01");
            }

            if (CurrentCombo == 2)
            {
                cooldownTimer = new CooldownTimer(280);
                input.animator.Play("Kick02");
            }
            if (CurrentCombo == 3)
            {
                cooldownTimer = new CooldownTimer(600);
                input.animator.Play("Kick03");
            }
        }
    }
}
