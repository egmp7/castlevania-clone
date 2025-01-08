using Game.Sensors;
using InputCommands.Buttons;
using InputCommands.Move;
using Player.StateManagement;
using UnityEngine;

namespace InputCommands
{
    [RequireComponent (typeof (StateMachine))]
    public class InputListener : MonoBehaviour
    {
        [SerializeField] private GroundSensor groundSensor;
        [SerializeField] private FallSensor fallSensor;

        // commands
        private ButtonCommand _punchCommand, _kickCommand, _blockCommand;
        private MoveCommand _idleCommand, _walkCommand, _runCommand, _jumpCommand, _crouchCommand, _flipDirectionCommand;
        private SensorCommand _fallCommand;

        // buttons
        private Button _buttonA, _buttonB, _buttonC;

        // utilities
        private DirectionMapper _directionMapper;
        private DoubleTapDetector _doubleTapDetector;
        private DirectionSwitch _directionSwitch;

        // state machine
        private StateMachine _stateMachine;

        private void Awake()
        {
            ValidateComponents();
        }

        private void Start()
        {
            try
            {
                InitializeCommands();
                InitializeMoveFeatures();
                InitializeButtons();
                InitializeStateMachine();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"{nameof(InputListener)}: Initialization failed - {ex.Message}");
                enabled = false; // Disable the script to avoid further issues.
            }
        }

        private void ValidateComponents()
        {
            Debug.Assert(groundSensor != null, $"{nameof(InputListener)}: GroundSensor is not assigned!");
            Debug.Assert(fallSensor != null, $"{nameof(InputListener)}: FallSensor is not assigned!");
        }

        private void InitializeCommands()
        {
            // Move commands
            _idleCommand = new IdleCommand();
            _walkCommand = new WalkCommand();
            _runCommand = new RunCommand();
            _jumpCommand = new JumpCommand();
            _crouchCommand = new CrouchCommand();
            _flipDirectionCommand = new FlipDirectionCommand();

            // Action commands
            _punchCommand = new PunchCommand();
            _kickCommand = new KickCommand();
            _blockCommand = new BlockCommand();

            // Sensor commands
            _fallCommand = new FallCommand();

            if (_idleCommand == null || _punchCommand == null)
            {
                throw new System.Exception("Command initialization failed. Ensure all commands are properly instantiated.");
            }
        }

        private void InitializeMoveFeatures()
        {
            _directionMapper = new DirectionMapper() ?? throw new MissingComponentException("DirectionMapper is not initialized!");
            _doubleTapDetector = new DoubleTapDetector() ?? throw new MissingComponentException("DoubleTapDetector is not initialized!");
            _directionSwitch = new DirectionSwitch() ?? throw new MissingComponentException("DirectionSwitch is not initialized!");
        }

        private void InitializeButtons()
        {
            _buttonA = new ButtonA();
            _buttonB = new ButtonB();
            _buttonC = new ButtonC();

            if (_buttonA == null || _buttonB == null || _buttonC == null)
            {
                throw new System.Exception("Button initialization failed. Ensure all buttons are properly instantiated.");
            }
        }

        private void InitializeStateMachine()
        {
            _stateMachine = GetComponent<StateMachine>();

            if (_stateMachine == null)
            {
                throw new System.Exception("StateMachine component is not available. This should not happen due to RequireComponent.");
            }
        }

        public MoveCommand GetMoveCommand()
        {
            // 
            if (groundSensor == null || _directionMapper == null || _directionSwitch == null)
            {
                Debug.LogWarning($"{nameof(InputListener)}: Missing components in GetMoveCommand.");
                return null;
            }

            // BLOCKS
            var currentState = _stateMachine.GetCurrentState();
            var airState = !groundSensor.GetState();
            if (airState) return null;
            if (currentState is AttackState) return null;
            if (currentState is HealthState) return null;
            if (currentState is BlockState) return null;


            var currentDirectionState = _directionMapper.GetState();
            var isDoubleTap = _doubleTapDetector?.Update(currentDirectionState) ?? false;

            if (_directionSwitch.Update(currentDirectionState))
                return _flipDirectionCommand;

            return currentDirectionState switch
            {
                DirectionMapper.State.Left or DirectionMapper.State.Right =>
                    isDoubleTap ? _runCommand : _walkCommand,

                DirectionMapper.State.Up or DirectionMapper.State.UpLeft or DirectionMapper.State.UpRight =>
                    _jumpCommand,

                DirectionMapper.State.Down or DirectionMapper.State.DownLeft or DirectionMapper.State.DownRight =>
                    _crouchCommand,

                _ => _idleCommand
            };
        }

        public ButtonCommand GetButtonCommand()
        {
            if (_stateMachine.GetCurrentState() is HealthState) return null;

            if (_buttonA == null || _buttonB == null || _buttonC == null)
            {
                Debug.LogWarning($"{nameof(InputListener)}: Buttons are not initialized.");
                return null;
            }

            if (_buttonA.IsPressed()) return _punchCommand;
            if (_buttonB.IsPressed()) return _kickCommand;
            if (_buttonC.IsPressed()) return _blockCommand;

            return null;
        }

        public MoveCommand GetReleaseCommand()
        {
            if (_buttonC.IsReleased()) return _idleCommand;
                
            return null;
        }

        public SensorCommand GetSensorCommand()
        {
            if (_stateMachine.GetCurrentState() is HealthState) return null;

            if (fallSensor == null)
            {
                Debug.LogWarning($"{nameof(InputListener)}: FallSensor is not assigned.");
                return null;
            }

            return fallSensor.GetState() ? _fallCommand : null;
        }
    }
}
