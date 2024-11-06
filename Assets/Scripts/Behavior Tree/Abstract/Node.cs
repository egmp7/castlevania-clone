namespace AI.BehaviorTree
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

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();

        private bool started;

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
