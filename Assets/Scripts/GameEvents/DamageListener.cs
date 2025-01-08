using Game.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Game.AnimationEvent.Receiver
{
    [RequireComponent(typeof (HealthManager))]

    public class DamageListener : MonoBehaviour
    {

        [SerializeField] private UnityEvent UnityEvent;

        private HealthManager _healthManager;
        private float _currentDamage;

        private void Awake()
        {
            _healthManager = GetComponent<HealthManager>();
            if (_healthManager != null )
            {
                Debug.LogError("Missing HealthManager Component");
            }
        }

        public void TakeDamage(float damage, LayerMask layerMask)
        {
            
            string layerNames = Utilities.GetLayerNames(layerMask);

            // Log the layer names and damage received
            Debug.Log($"Damage received: {damage}, Layer(s): {layerNames}");
            _currentDamage = damage;
            _healthManager.DecreaseHealth(damage);

            UnityEvent.Invoke();
        }

        public float GetCurrentDamage()
        {
            return _currentDamage;
        }
    }
}
