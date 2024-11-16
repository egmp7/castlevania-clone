using Player.StateManagement;

namespace InputCommands
{

    public abstract class ReusableCommand
    {
        public abstract void Execute(StateController receiver);
    }

    public class IdleCommand : ReusableCommand
    {
        public override void Execute(StateController receiver)
        {
            receiver.Idle();
        }
    }

    public class WalkCommand : ReusableCommand
    {
        public override void Execute(StateController receiver)
        {
            receiver.Walk();
        }
    }

    public class RunCommand : ReusableCommand
    {
        public override void Execute(StateController receiver)
        {
            receiver.Run();
        }
    }

    public class JumpCommand : ReusableCommand
    {
        public override void Execute(StateController receiver)
        {
            receiver.Jump();
        }
    }

    public class CrouchCommand : ReusableCommand
    {
        public override void Execute(StateController receiver)
        {
            receiver.Crouch();
        }
    }
}