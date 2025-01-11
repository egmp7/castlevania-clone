using BehaviorDesigner.Runtime.Tasks;
using Game.Managers;

namespace Enemy.AI
{
    public abstract class EnemyConditional : Conditional
    {
        protected HealthManagerBT _healthManager;

        public override void OnAwake()
        {
            _healthManager = GetComponent<HealthManagerBT>();
            if (_healthManager == null)
            {
                ErrorManager.LogMissingComponent<HealthManagerBT>(gameObject);
            }
        }
    }
}