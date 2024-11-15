using GameProject.Utilities;

namespace Player.StateManagement
{

    public class PunchState : ComboState
    {
        private static CooldownTimer cooldownTimer = new (0);

        protected override void OnEnter()
        {
            base.OnEnter();
            MaxCombo = 3;
            ComboResetTime = input.PunchComboResetTime;
        }

        protected override void OnAttack()
        {
            if (!cooldownTimer.IsCooldownComplete()) return;

            base.OnAttack();
            
            if (CurrentCombo == 1 )
            {
                cooldownTimer = new CooldownTimer(300);
                input.animator.Play("Punch01");
            }

            if (CurrentCombo == 2)
            {
                cooldownTimer = new CooldownTimer(150);
                input.animator.Play("Punch02");
            }

            if (CurrentCombo == 3)
            {
                cooldownTimer = new CooldownTimer(220);
                input.animator.Play("Punch03");
            }
        }

    }
}
