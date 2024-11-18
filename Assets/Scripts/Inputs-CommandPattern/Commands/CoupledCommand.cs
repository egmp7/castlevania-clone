using Player.StateManagement;

namespace InputCommands
{

    public abstract class CoupledCommand
    {
        public abstract void Execute(StateMachine receiver);
    }

    public class PunchCommand : CoupledCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Punch();
        }
    }

    public class KickCommand : CoupledCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Kick();
        }
    }
}