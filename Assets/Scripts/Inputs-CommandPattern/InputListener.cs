using UnityEngine;
using UnityEngine.InputSystem;
using InputCommands.Move;
using Game.Sensors;

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

        private InputAction actionA, actionB;

        private DirectionMapper directionMapper;
        private DoubleTapDetector doubleTapDetector;

        private void Awake()
        {
            actionA = InputSystem.actions.FindAction("ActionA");
            actionB = InputSystem.actions.FindAction("ActionB");
        }

        void Start()
        {
            directionMapper = GetComponent<DirectionMapper>();
            doubleTapDetector = new DoubleTapDetector();

            idle = new IdleCommand();
            walk = new WalkCommand();
            run = new RunCommand();
            jump = new JumpCommand();
            crouch = new CrouchCommand();

            punch = new PunchCommand();
            kick = new KickCommand();

            fall = new FallCommand();
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

        public SensorCommand GetSensorCommands()
        {
            if (FallSensor.GetState()) return fall;

            return null;
        }
    }
}