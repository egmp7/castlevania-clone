using Player.StateManagement;

namespace InputCommands
{

    public abstract class ReusableCommand
    {
        public abstract void Execute(StateMachine receiver);
    }

    public class IdleCommand : ReusableCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Idle();
        }
    }

    public class WalkCommand : ReusableCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Walk();
        }
    }

    public class RunCommand : ReusableCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Run();
        }
    }

    public class JumpCommand : ReusableCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Jump();
        }
    }

    public class CrouchCommand : ReusableCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Crouch();
        }
    }
}