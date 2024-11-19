using Player.StateManagement;

namespace InputCommands
{

    public abstract class MoveCommand
    {
        public abstract void Execute(StateMachine receiver);
    }

    public class IdleCommand : MoveCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Idle();
        }
    }

    public class WalkCommand : MoveCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Walk();
        }
    }

    public class RunCommand : MoveCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Run();
        }
    }

    public class JumpCommand : MoveCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Jump();
        }
    }

    public class CrouchCommand : MoveCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.Crouch();
        }
    }

    public class FlipDirectionCommand : MoveCommand
    {
        public override void Execute(StateMachine receiver)
        {
            receiver.FlipDirection();
        }
    }
}