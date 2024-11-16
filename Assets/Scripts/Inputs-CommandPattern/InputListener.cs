using UnityEngine;
using UnityEngine.InputSystem;
using InputCommands.Move;

namespace InputCommands
{
    public class InputListener : MonoBehaviour
    {
        private CoupledCommand punch, kick;
        private ReusableCommand idle, walk, run, jump, crouch;

        private InputAction moveAction, actionA, actionB;

        private DirectionMapper directionMapper;
        private DirectionDetector directionDetector;
        private DoubleTapDetector doubleTapDetector;

        private void Awake()
        {
            moveAction = InputSystem.actions.FindAction("Move");
            actionA = InputSystem.actions.FindAction("ActionA");
            actionB = InputSystem.actions.FindAction("ActionB");
        }

        void Start()
        {
            directionMapper = new DirectionMapper();
            directionDetector = new DirectionDetector();
            doubleTapDetector = new DoubleTapDetector();

            idle = new IdleCommand();
            walk = new WalkCommand();
            run = new RunCommand();
            jump = new JumpCommand();
            crouch = new CrouchCommand();

            punch = new PunchCommand();
            kick = new KickCommand();
        }

        public ReusableCommand GetReusableCommands()
        {
            Vector2 moveInput;
            DirectionMapper.State currentDMState;
            bool isDoubleTap;

            moveInput = moveAction.ReadValue<Vector2>();
            currentDMState = directionMapper.Update(moveInput);
            isDoubleTap = doubleTapDetector.Update(currentDMState);
            directionDetector.Update(currentDMState);

            if (currentDMState == DirectionMapper.State.Left ||
                currentDMState == DirectionMapper.State.Right)
            {
                if (isDoubleTap)
                {
                    return run;
                }
                else
                {
                    return walk;
                }
            }

            if (currentDMState == DirectionMapper.State.Up ||
                currentDMState == DirectionMapper.State.UpLeft ||
                currentDMState == DirectionMapper.State.UpRight)
            {
                return jump;
            }

            if (currentDMState == DirectionMapper.State.Down ||
                currentDMState == DirectionMapper.State.DownLeft ||
                currentDMState == DirectionMapper.State.DownRight)
            {
                return crouch;
            }

                return idle;
        }

        public CoupledCommand GetCoupledCommands()
        {
            if (actionA.IsPressed())
            {
                return punch;
            }

            if (actionB.IsPressed())
            {
                return kick;
            }

            return null;
        }
    }
}