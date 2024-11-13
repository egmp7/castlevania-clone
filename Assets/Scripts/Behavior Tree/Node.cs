using AI.BehaviorTree.Runners;

namespace AI.BehaviorTree.Nodes
{
    public abstract class Node
    {
        public enum State
        {
            Running,
            Success,
            Failure
        }

        protected State state = State.Running;
        protected Runner runner;
        private bool started;

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();

        public void SetUp(Runner input)
        {
            runner = input;
        }

        public State Update()
        {
            // if the node has not started Start the Node.
            if (!started)
            {
                OnStart();
                started = true;
            }

            // set the state and run the nodes child update logic
            state = OnUpdate();

            if (state == State.Failure || state == State.Success)
            {
                OnStop();
                started = false;
            }

            return state;
        }

    }
}
