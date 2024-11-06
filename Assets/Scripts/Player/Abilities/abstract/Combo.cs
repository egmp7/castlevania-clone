using System;
using UnityEngine;

namespace Player.StateManagement
{

    public abstract class ComboAbility : Ability
    {
        private int currentCombo;
        private int maxCombo;
        private float lastTimeUsed;
        private float comboResetTime;

        public void SetUp(int maxCombo, float comboResetTime)
        {
            this.maxCombo = maxCombo;
            this.comboResetTime = comboResetTime;
        }

        public void InvokeComboState(Action[] comboStates)
        {

            // Invoke the corresponding action for the current combo stage
            if (currentCombo - 1 < comboStates.Length)
            {
                comboStates[currentCombo - 1]?.Invoke();
            }
            else
            {
                Debug.LogError("Array out of boundaries");
            }
        }

        public override void Use()
        {
            lastTimeUsed = Time.time;

            // Cycle through combo stages
            currentCombo = (currentCombo % maxCombo) + 1;

            Debug.Log("Current Combo: " + currentCombo);
        }

        public override void Update()
        {
            if (Time.time - lastTimeUsed > comboResetTime)
            {
                currentCombo = 0;
            }

        }

        public void ResetComboState()
        {
            currentCombo = 0;
        }

    }
}
