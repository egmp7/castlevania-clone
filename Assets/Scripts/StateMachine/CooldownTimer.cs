using System.Threading.Tasks;

namespace GameProject.Utilities
{
    public class CooldownTimer
    {
        private readonly int coolDownTime;
        private bool inUse;

        public CooldownTimer(int coolDownTime = 1000)
        {
            this.coolDownTime = coolDownTime;
            _ = CoolDown();
        }

        private async Task CoolDown()
        {
            inUse = true;

            await Task.Delay(coolDownTime);

            inUse = false;
        }

        public bool IsCooldownComplete()
        {
            return !inUse;
        }
    }
}
