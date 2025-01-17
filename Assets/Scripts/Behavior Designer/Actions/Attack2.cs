using egmp7.Game.Combat;
using Enemy.AI;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class Attack2Action : EnemyAction
    {
        [Tooltip("The minimum wait time if random wait is enabled")]
        public SharedFloat randomWaitMin = 1;
        [Tooltip("The maximum wait time if random wait is enabled")]
        public SharedFloat randomWaitMax = 1;

        // The time to wait
        private float _waitDuration;
        // The time that the task started to wait.
        private float _startTime;

        private int _currentCombo;
        private int _maxCombo;

        private float _attackRadius;
        private float _attackAmount;
        private Vector2 _attackOffset;
        private readonly string _attackFrom = "Enemy";
        private readonly string _attackTo = "Player";

        public override void OnStart()
        {
            base.OnStart();
            _maxCombo = 3;
            _attackRadius = 0.5f;
            _rb.velocity = Vector2.zero;
            _animator.Play("Combat Idle");
            _startTime = Time.time;
            _waitDuration = Random.Range(randomWaitMin.Value, randomWaitMax.Value);
        }

        public override TaskStatus OnUpdate()
        {
            _rb.velocity = Vector2.zero;

            // The task is done waiting if the time waitDuration has elapsed since the task was started.
            if (_startTime + _waitDuration < Time.time)
            {
                Attack();
            }
            // Otherwise we are still waiting.
            return TaskStatus.Running;
        }

        private void Attack()
        {
            // update last time used
            _startTime = Time.time;

            // Cycle through combo stages
            _currentCombo = (_currentCombo % _maxCombo) + 1;

            if (_currentCombo == 1)
            {
                _attackAmount = 20;
                _attackOffset = new Vector2(0.3f, -0.3f);
                _animator.Play("Punch01", -1, 0f);
            }

            if (_currentCombo == 2)
            {
                _attackAmount = 10;
                _attackOffset = new Vector2(0.3f, 0.5f);
                _animator.Play("Punch02", -1, 0f);
            }

            if (_currentCombo == 3)
            {
                _attackAmount = 33;
                _attackOffset = new Vector2(0.1f, 1f);
                _animator.Play("Punch03", -1, 0f);
            }

            Attack attack = new(
                _attackOffset,
                _attackRadius,
                _attackAmount,
                _attackFrom,
                _attackTo);
            _processor.SetFromAttack(attack);
        }
    }
}