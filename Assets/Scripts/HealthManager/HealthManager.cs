using UnityEngine;

namespace Game.Managers
{
    public abstract class HealthManager : MonoBehaviour
    {
        public float initValue = 500;
        public float blockDamageReducer = 0.1f;

        protected float _currentHealth;
        protected float _initHealth;

        private void Start()
        {
            _currentHealth = initValue;
            _initHealth = initValue;
        }

        public float GetCurrentHealth()
        {
            return _currentHealth;
        }

        public float GetHealthPercentage()
        {
            return _currentHealth / _initHealth;
        }
    }
}

