using Game.Sensors;
using InputCommands.Buttons;
using InputCommands.Move;
using UnityEngine;

namespace InputCommands
{

    public class InputListener : MonoBehaviour
    {
        [SerializeField] GroundSensor GroundSensor;
        [SerializeField] FallSensor FallSensor;

        private ButtonCommand _punch, _kick;
        private MoveCommand _idle, _walk, _run, _jump, _crouch, _flipDirection;
        private SensorCommand _fall;

        private Button _buttonA, _buttonB, _buttonC;

        private DirectionMapper _MFdirectionMapper;
        private DoubleTapDetector _MFdoubleTapDetector;
        private DirectionSwitch _MFdirectionSwitch;

        void Start()
        {

            // move commands
            _idle = new IdleCommand();
            _walk = new WalkCommand();
            _run = new RunCommand();
            _jump = new JumpCommand();
            _crouch = new CrouchCommand();
            _flipDirection = new FlipDirectionCommand(); 

            // action commands
            _punch = new PunchCommand();
            _kick = new KickCommand();

            // sensor commands
            _fall = new FallCommand();
            
            // move features
            _MFdirectionMapper = new DirectionMapper();
            _MFdoubleTapDetector = new DoubleTapDetector();
            _MFdirectionSwitch = new DirectionSwitch();

            // buttons
            _buttonA = new ButtonA();
            _buttonB = new ButtonB();
            _buttonC = new ButtonC();
        }

        public MoveCommand GetMoveCommands()
        {
            if (!GroundSensor.GetState()) return null;

            DirectionMapper.State currentDMState;
            bool isDoubleTap;

            currentDMState = _MFdirectionMapper.GetState();
            isDoubleTap = _MFdoubleTapDetector.Update(currentDMState);
            
            if(_MFdirectionSwitch.Update(currentDMState)) return _flipDirection; 

            if (currentDMState == DirectionMapper.State.Left ||
                currentDMState == DirectionMapper.State.Right)
            {
                if (isDoubleTap)
                {
                    return _run;
                }
                else
                {
                    return _walk;
                }
            }

            if (currentDMState == DirectionMapper.State.Up ||
                currentDMState == DirectionMapper.State.UpLeft ||
                currentDMState == DirectionMapper.State.UpRight)
            {
                return _jump;
            }

            if (currentDMState == DirectionMapper.State.Down ||
                currentDMState == DirectionMapper.State.DownLeft ||
                currentDMState == DirectionMapper.State.DownRight)
            {
                return _crouch;
            }

                return _idle;
        }

        public ButtonCommand GetButtonCommands()
        {
            if (_buttonA.IsPressed())
            {
                return _punch;
            }

            if (_buttonB.IsPressed())
            {
                return _kick;
            }

            if (_buttonC.IsPressed())
            {
                return null;
            }

            return null;
        }

        public SensorCommand GetSensorCommands()
        {
            if (FallSensor.GetState()) return _fall;

            return null;
        }
    }
}