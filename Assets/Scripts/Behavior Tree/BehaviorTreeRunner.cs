using UnityEngine;

namespace AI.BehaviorTree {

    public class BehaviorTreeRunner : MonoBehaviour
    {
        public ZoneSensor AttackZoneSensor;

        private BehaviorTree tree = new();


        private void Start()
        {
            DebugLogNode log = new(this)
            {
                message = "Test"
            };

            IsPlayerInRange isPlayerInRange = new(this);

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

            tree.rootNode = isPlayerInRange;
        }

        void Update()
        {
            tree.Update();
        }
    }
}