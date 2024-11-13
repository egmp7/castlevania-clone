using UnityEngine;
using AI.BehaviorTree.Nodes;
using Game.Sensors;

namespace AI.BehaviorTree.Runners
{

    public class EnemyRunner : Runner
    {
        [SerializeField] ColliderSensor ColliderSensor;

        private readonly BehaviorTree behaviorTree = new();

        private void Start()
        {
            ConditionNode conditionNode = new(ColliderSensor.GetActiveZone);
            AttackNode attackNode = new();
            PatrolNode patrolNode = new();

            SequencerNode sequencerNode = new();
            SelectorNode selectorNode = new();
            RepeatNode repeatNode = new();

            sequencerNode.children.Add(conditionNode);
            sequencerNode.children.Add(attackNode);

            selectorNode.children.Add(sequencerNode);
            selectorNode.children.Add(patrolNode);

            repeatNode.child = selectorNode;

            behaviorTree.rootNode = repeatNode;
        }

        void Update()
        {
            behaviorTree.Update();
        }
    }
}