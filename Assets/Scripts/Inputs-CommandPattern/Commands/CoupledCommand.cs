using Player.StateManagement;

namespace InputCommands
{

    public abstract class CoupledCommand
    {
        public abstract void Execute(StateController receiver);
    }

    public class PunchCommand : CoupledCommand
    {
        public override void Execute(StateController receiver)
        {
            receiver.Punch();
        }
    }

    public class KickCommand : CoupledCommand
    {
        public override void Execute(StateController receiver)
        {
            receiver.Kick();
        }
    }
}