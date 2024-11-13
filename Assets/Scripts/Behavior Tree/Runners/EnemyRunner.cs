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
            RepeatNode repeatNode = new()
            {
                child = conditionNode
            };

            behaviorTree.rootNode = repeatNode;
        }

        void Update()
        {
            behaviorTree.Update();
        }


        //DebugLogNode log = new(this)
        //{
        //    message = "Test"
        //};

        //IsPlayerInRange isPlayerInRange = new(this);

        //DebugLogNode log1 =
        //  ScriptableObject.CreateInstance<DebugLogNode>();
        //log1.message = "Testing 1";
        //DebugLogNode log2 =
        //  ScriptableObject.CreateInstance<DebugLogNode>();
        //log2.message = "Testing 2";
        //DebugLogNode log3 = ScriptableObject.CreateInstance<DebugLogNode>();
        //log3.message = "Testing 3";

        //WaitNode wait1 =
        //  ScriptableObject.CreateInstance<WaitNode>();
        //ActionNode wait2 =
        //  ScriptableObject.CreateInstance<WaitNode>();
        //ActionNode wait3 =
        //  ScriptableObject.CreateInstance<WaitNode>();

        //CompositeNode sequence =
        //  ScriptableObject.CreateInstance<SequencerNode>();
        //sequence.children.Add(log1);
        //sequence.children.Add(wait1);
        //sequence.children.Add(log2);
        //sequence.children.Add(wait2);
        //sequence.children.Add(log3);
        //sequence.children.Add(wait3);

        //DecoratorNode loop =
        //  ScriptableObject.CreateInstance<RepeatNode>();
        //loop.child = sequence;

        //m_tree.rootNode = loop;

        //tree.rootNode = isPlayerInRange;
    }
}