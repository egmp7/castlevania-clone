using UnityEngine;

namespace Player.StateManagement
{

    public abstract class ComboState : AttackState
    {
        protected int CurrentCombo;
        protected float LastTimeUsed;
        protected int MaxCombo;
        protected float ComboResetTime;

        protected override void OnAttack()
        {
            base.OnAttack();
            LastTimeUsed = Time.time;

            // Cycle through combo stages
            CurrentCombo = (CurrentCombo % MaxCombo) + 1;

            // Debug.Log("Current Combo: " + CurrentCombo);
        }

        protected override void OnUpdate()
        {
            if (Time.time - LastTimeUsed > ComboResetTime)
            {
                CurrentCombo = 0;
            }
        }

        public void ResetComboState()
        {
            CurrentCombo = 0;
        }

    }

}
