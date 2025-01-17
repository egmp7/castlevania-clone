using egmp7.Game.Combat;
using egmp7.Game.Manager;
using Enemy.AI;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class AttackAction : EnemyAction
    {
        [Tooltip("The minimum wait time if random wait is enabled")]
        public SharedFloat randomWaitMin = 1;
        [Tooltip("The maximum wait time if random wait is enabled")]
        public SharedFloat randomWaitMax = 1;
        [Tooltip("General attack damage")]
        public SharedFloat attackDamage = 20;
        [Tooltip("Increment ratio for the attack damage")]
        public SharedFloat attackSlope = 2;
        [Tooltip("Increment ratio for the attack damage")]
        public SharedFloat waitSlope = 1;

        // The time to wait
        private float _waitDuration;
        // The time that the task started to wait.
        private float _startTime;

        private int _currentCombo;
        private int _maxCombo;

        private float _attackRadius;
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
        }

        public override TaskStatus OnUpdate()
        {
            var minWait = randomWaitMin.Value - (MainManager.Instance.difficulty * waitSlope.Value);
            var maxWait = randomWaitMax.Value - (MainManager.Instance.difficulty * waitSlope.Value);

            _waitDuration = Random.Range(minWait, maxWait);
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
            float attackAmount;
            float comboAmount = 0;

            // update last time used
            _startTime = Time.time;

            // Cycle through combo stages
            _currentCombo = (_currentCombo % _maxCombo) + 1;

            if (_currentCombo == 1)
            {
                comboAmount = 1f;
                _attackOffset = new Vector2(0.3f, -0.3f);
                _animator.Play("Punch01", -1, 0f);
            }

            if (_currentCombo == 2)
            {
                comboAmount = 0.75f;
                _attackOffset = new Vector2(0.3f, 0.5f);
                _animator.Play("Punch02", -1, 0f);
            }

            if (_currentCombo == 3)
            {
                comboAmount = 1.25f;
                _attackOffset = new Vector2(0.1f, 1f);
                _animator.Play("Punch03", -1, 0f);
            }

            attackAmount =
                attackDamage.Value +
                attackDamage.Value * comboAmount * (MainManager.Instance.difficulty) * attackSlope.Value; 

            Attack attack = new(
                _attackOffset,
                _attackRadius,
                attackAmount,
                _attackFrom,
                _attackTo);
            _processor.SetFromAttack(attack);
        }
    }
}