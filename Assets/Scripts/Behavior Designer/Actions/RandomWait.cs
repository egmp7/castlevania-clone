using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class RandomWait : EnemyAction
    {
        public SharedBool sharedBool;
        public float randomWaitMin = 1;
        public float randomWaitMax = 1;

        // The time to wait
        private float _waitDuration;
        // The time that the task started to wait.
        private float _startTime;
        // Remember the time that the task is paused so the time paused doesn't contribute to the wait time.

        public override void OnAwake()
        {
            _waitDuration = Random.Range(randomWaitMin, randomWaitMax);
        }

        public override TaskStatus OnUpdate()
        {
            
            if (sharedBool.Value)
            {
                _startTime = Time.time;
            }
            else
            {
                if (_startTime + _waitDuration < Time.time)
                {
                    sharedBool.Value = false;
                    return TaskStatus.Failure;
                }
            }

            sharedBool.Value = false;
            return TaskStatus.Success;
        }
    }
}
