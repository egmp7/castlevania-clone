using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace egmp7.BehaviorDesigner.Enemy
{
    public abstract class EnemyAction : Action
    {
        protected Rigidbody2D _rb;
        protected GameObject _player;

        private readonly string _playerTag = "Player";

        public override void OnAwake()
        {
            ValidateFields();
        }

        private void ValidateFields()
        {
            _rb = GetComponent<Rigidbody2D>();
            if (_rb == null)
            {
                ErrorManager.LogMissingComponent<Rigidbody2D>(gameObject);
            }

            _player = GameObject.FindGameObjectWithTag(_playerTag);
            if (_player == null)
            {
                ErrorManager.LogMissingGameObjectWithTag(_playerTag);
            }
        }
    }
}
