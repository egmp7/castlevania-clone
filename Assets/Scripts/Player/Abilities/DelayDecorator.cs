using System.Threading.Tasks;
using UnityEngine;

namespace Player.StateManagement
{

    public class DelayComboDecorator : ComboAbility
    {
        private readonly ComboAbility comboAbility;
        private readonly int coolDownTime;
        private bool inUse;

        public DelayComboDecorator(ComboAbility comboAbility, int coolDownTime = 1000)
        {
            this.comboAbility = comboAbility;
            this.coolDownTime = coolDownTime;
        }

        public override void Use()
        {
            if (inUse) return;

            base.Use();
            _ = CoolDown();
        }

        private async Task CoolDown()
        {
            inUse = true;
            Debug.Log("Cooling Down Ability");

            await Task.Delay(coolDownTime);

            Debug.Log($"{comboAbility.GetType().Name} can now be used.");

            inUse = false;
        }
    }
}

