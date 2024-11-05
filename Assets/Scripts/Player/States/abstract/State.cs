namespace Player.StateManagement
{
    public abstract class State
    {
        protected StateController input;

        public void OnStateEnter(StateController stateController)
        {
            input = stateController;
            OnEnter();
        }

        protected virtual void OnEnter()
        {
        }

        public void OnStateUpdate()
        {
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
        }

        public void OnStateFixedUpdate()
        {
            OnFixedUpdate();
        }

        protected virtual void OnFixedUpdate()
        {
        }

        public void OnStateExit()
        {
            OnExit();
        }

        protected virtual void OnExit()
        {
        }
    }
}