namespace Player.StateManagement
{
    public abstract class State
    {
        protected StateMachine input;

        public void OnStateEnter(StateMachine stateMachine)
        {
            input = stateMachine;
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

        public void OnStateAttack()
        {
            OnAttack();
        }

        protected virtual void OnAttack()
        {
        }
    }
}