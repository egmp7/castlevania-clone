using Player.StateManagement;

namespace InputCommands
{

    public abstract class ButtonCommand
    {
        public abstract void Execute(StateMachine receiver);
    }

    public class PunchCommand : ButtonCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Punch();
        }
    }

    public class KickCommand : ButtonCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Kick();
        }
    }
}