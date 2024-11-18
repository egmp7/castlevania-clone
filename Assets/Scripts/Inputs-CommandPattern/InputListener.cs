using Game.Sensors;
using InputCommands.Buttons;
using InputCommands.Move;
using UnityEngine;

namespace InputCommands
{

    [RequireComponent(typeof(DirectionMapper))]
    public class InputListener : MonoBehaviour
    {
        [SerializeField] GroundSensor GroundSensor;
        [SerializeField] FallSensor FallSensor;

        private ButtonCommand punch, kick;
        private MoveCommand idle, walk, run, jump, crouch;
        private SensorCommand fall;

        private Button buttonA, buttonB, buttonC;

        private DirectionMapper directionMapper;
        private DoubleTapDetector doubleTapDetector;

        void Start()
        {
            directionMapper = GetComponent<DirectionMapper>();
            doubleTapDetector = new DoubleTapDetector();

            // move commands
            idle = new IdleCommand();
            walk = new WalkCommand();
            run = new RunCommand();
            jump = new JumpCommand();
            crouch = new CrouchCommand();

            // action commands
            punch = new PunchCommand();
            kick = new KickCommand();

            // sensor commands
            fall = new FallCommand();

            // buttons
            buttonA = new ButtonA();
            buttonB = new ButtonB();
            buttonC = new ButtonC();
        }

        public MoveCommand GetMoveCommands()
        {
            if (!GroundSensor.GetState()) return null;

            DirectionMapper.State currentDMState;
            bool isDoubleTap;

            currentDMState = directionMapper.GetState();
            isDoubleTap = doubleTapDetector.Update(currentDMState);

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

        public ButtonCommand GetButtonCommands()
        {
            if (buttonA.IsPressed())
            {
                return punch;
            }

            if (buttonB.IsPressed())
            {
                return kick;
            }

            if (buttonC.IsPressed())
            {
                return null;
            }

            return null;
        }

        public SensorCommand GetSensorCommands()
        {
            if (FallSensor.GetState()) return fall;

            return null;
        }
    }
}