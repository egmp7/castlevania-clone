using BehaviorDesigner.Runtime.Tasks;
using egmp7.Game.Combat;
using GameProject.Utilities;
using UnityEngine;

namespace Enemy.AI
{
    public class AttackAction : EnemyAction
    {
        public float attackChance = 0.01f;
        public float minimumWaitTime = 1f;
        public Transform attackOffset;
        public LayerMask playerLayer;

        private int _currentCombo;
        private float _lastTimeUsed;
        private int _maxCombo;

        private float _attackRadius;
        private float _attackAmount;
        private Vector2 _attackOffset;
        private readonly string _attackFrom = "Enemy";
        private readonly string _attackTo = "Player";

        private static CooldownTimer cooldownTimer = new(0);

        public override void OnStart()
        {
            base.OnStart();
            _maxCombo = 3;
            _attackRadius = 0.5f;
        }

        public override TaskStatus OnUpdate()
        {
            _rb.velocity = Vector2.zero;
            //_animator.Play("Combat Idle");

            // Attack
            if (
                // Generate a random value between 0 and 1
                Random.value < attackChance &&
                // Cooldown time
                Time.time > (_lastTimeUsed + minimumWaitTime))
            {
                Attack();
            }

            return TaskStatus.Success;
        }

        private void Attack()
        {
            // update last time used
            _lastTimeUsed = Time.time;

            // Cycle through combo stages
            _currentCombo = (_currentCombo % _maxCombo) + 1;

            if (!cooldownTimer.IsCooldownComplete()) return;

            if (_currentCombo == 1)
            {
                cooldownTimer = new CooldownTimer(300);
                _attackAmount = 20;
                _attackOffset = new Vector2(0.3f, -0.3f);
                _animator.Play("Punch01",-1,0f);
            }

            if (_currentCombo == 2)
            {
                cooldownTimer = new CooldownTimer(150);
                _attackAmount = 10;
                _attackOffset = new Vector2(0.3f, 0.5f);
                _animator.Play("Punch02",-1,0f);
            }

            if (_currentCombo == 3)
            {
                cooldownTimer = new CooldownTimer(220);
                _attackAmount = 33;
                _attackOffset = new Vector2(0.1f, 1f);
                _animator.Play("Punch03",-1,0f);
            }

            Attack attack = new (
                _attackOffset,
                _attackRadius,
                _attackAmount,
                _attackFrom,
                _attackTo);
            _processor.SetFromAttack(attack);

        }
    }
}