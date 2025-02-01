using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace egmp7.BehaviorDesigner.Enemy
{
    public abstract class EnemyAction : Action
    {
        protected Rigidbody2D _rb;
        protected GameObject _player;
        protected Animator _animator;

        private readonly string _playerTag = "Player";

        public override void OnAwake()
        {
            ValidateFields();
        }

        private void ValidateFields()
        {
            var logActionName = $"Action: {FriendlyName}";

            _rb = GetComponent<Rigidbody2D>();
            if (_rb == null)
            {
                ErrorManager.LogMissingComponent<Rigidbody2D>(gameObject, logActionName);
            }

            _animator = gameObject.GetComponentInChildren<Animator>();
            if (_animator == null)
            {
                ErrorManager.LogMissingComponent<Animator>(gameObject, logActionName);
            }

            _player = GameObject.FindGameObjectWithTag(_playerTag);
            if (_player == null)
            {
                ErrorManager.LogMissingGameObjectWithTag(_playerTag, logActionName);
            }
        }
    }
}
