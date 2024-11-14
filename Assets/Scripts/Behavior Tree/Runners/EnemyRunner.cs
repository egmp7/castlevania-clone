using UnityEngine;
using AI.BehaviorTree.Nodes;
using Game.Sensors;

namespace AI.BehaviorTree.Runners
{

    public class EnemyRunner : Runner
    {
        [SerializeField] ColliderSensor PursueSensor;
        [SerializeField] ColliderSensor AttackSensor;

        private readonly BehaviorTree behaviorTree = new();

        private void Start()
        {
            ConditionNode isPlayerOnPursueZone = new(PursueSensor.GetActiveZone);
            ConditionNode isPlayerOnAttackZone = new(AttackSensor.GetActiveZone);
            AttackNode attackNode = new();
            PatrolNode patrolNode = new();
            MoveNode moveNode = new();

            SequencerNode sequencerNode00 = new();
            SequencerNode sequencerNode01 = new();

            SelectorNode selectorNode00 = new();
            SelectorNode selectorNode01 = new();
            RepeatNode repeatNode = new();

            sequencerNode00.children.Add(isPlayerOnAttackZone);
            sequencerNode00.children.Add(attackNode);
            sequencerNode01.children.Add(isPlayerOnPursueZone);
            sequencerNode01.children.Add(moveNode);

            selectorNode00.children.Add(selectorNode01);
            selectorNode00.children.Add(patrolNode);

            selectorNode01.children.Add(sequencerNode00);
            selectorNode01.children.Add(sequencerNode01);

            repeatNode.child = selectorNode00;

            behaviorTree.rootNode = repeatNode;

            // Set Up
            patrolNode.SetUp(this);
            attackNode.SetUp(this);
            moveNode.SetUp(this);
        }

        void Update()
        {
            behaviorTree.Update();
        }
    }
}