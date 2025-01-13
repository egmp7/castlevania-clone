using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Game.AnimationEvent.Source;
using Game.Managers;

namespace Enemy.AI
{
    public abstract class EnemyAction : Action
    {
        protected Rigidbody2D _rb;
        protected Animator _animator;
        protected Transform _playerTransform;
        protected DamageProcessor _processor;
        protected HealthManagerBT _healthManager;

        private readonly string _playerTag = "Player";

        public override void OnAwake()
        {
            _rb = GetComponent<Rigidbody2D>();
            if (_rb == null)
            {
                ErrorManager.LogMissingComponent<Rigidbody2D>(gameObject);
            }

            _animator = gameObject.GetComponentInChildren<Animator>();
            if (_animator == null)
            {
                ErrorManager.LogMissingComponent<Animator>(gameObject);
            }

            _processor = GetComponent<DamageProcessor>();
            if (_processor == null)
            {
                ErrorManager.LogMissingComponent<DamageProcessor>(gameObject);
            }

            _healthManager = GetComponent<HealthManagerBT>();
            if (_healthManager == null)
            {
                ErrorManager.LogMissingComponent<HealthManagerBT>(gameObject);
            }

            _playerTransform = GameObject.FindGameObjectWithTag(_playerTag).transform;
            if (_playerTransform == null)
            {
                ErrorManager.LogMissingGameObjectWithTag(_playerTag);
            }
        }
    }
}
