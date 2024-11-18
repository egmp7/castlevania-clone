using Player.StateManagement;

namespace InputCommands
{

    public abstract class SensorCommand
    {
        public abstract void Execute(StateMachine receiver);
    }

    public class FallCommand : SensorCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Fall();
        }
    }
}