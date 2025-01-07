using BehaviorDesigner.Runtime.Tasks;
using GameProject.Utilities;
using UnityEngine;

namespace Enemy.AI
{
    public class AttackPlayer : EnemyAction
    {
        public float attackChance = 0.1f;
        public float minimumWaitTime = 1.5f;
        public Transform attackOffset;
        public LayerMask playerLayer;

        private int _currentCombo;
        private float _lastTimeUsed;
        private int _maxCombo;

        private static CooldownTimer cooldownTimer = new(0);

        public override void OnStart()
        {
            base.OnStart();
            _maxCombo = 3;
            processor.attackRadius = 0.5f;
        }

        public override TaskStatus OnUpdate()
        {

            // Generate a random value between 0 and 1
            if (Random.value < attackChance &&
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
                processor.currentDamage = 20;
                processor.offset = new Vector2(0.3f, -0.3f);
                animator.Play("Punch01");
            }

            if (_currentCombo == 2)
            {
                cooldownTimer = new CooldownTimer(150);
                processor.currentDamage = 10;
                processor.offset = new Vector2(0.3f, 0.5f);
                animator.Play("Punch02");
            }

            if (_currentCombo == 3)
            {
                cooldownTimer = new CooldownTimer(220);
                processor.currentDamage = 33;
                processor.offset = new Vector2(0.1f, 1f);
                animator.Play("Punch03");
            }

        }
    }
}