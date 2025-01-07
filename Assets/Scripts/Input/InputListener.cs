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

        private ButtonCommand punchCommand, kickCommand;
        private MoveCommand idleCommand, walkCommand, runCommand, jumpCommand, crouchCommand, flipDirectionCommand;
        private SensorCommand fallCommand;

        private Button buttonA, buttonB, buttonC;

        private DirectionMapper directionMapper;
        private DoubleTapDetector doubleTapDetector;
        private DirectionSwitch directionSwitch;

        private StateMachine stateMachine;

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
            idleCommand = new IdleCommand();
            walkCommand = new WalkCommand();
            runCommand = new RunCommand();
            jumpCommand = new JumpCommand();
            crouchCommand = new CrouchCommand();
            flipDirectionCommand = new FlipDirectionCommand();

            // Action commands
            punchCommand = new PunchCommand();
            kickCommand = new KickCommand();

            // Sensor commands
            fallCommand = new FallCommand();

            if (idleCommand == null || punchCommand == null)
            {
                throw new System.Exception("Command initialization failed. Ensure all commands are properly instantiated.");
            }
        }

        private void InitializeMoveFeatures()
        {
            directionMapper = new DirectionMapper() ?? throw new MissingComponentException("DirectionMapper is not initialized!");
            doubleTapDetector = new DoubleTapDetector() ?? throw new MissingComponentException("DoubleTapDetector is not initialized!");
            directionSwitch = new DirectionSwitch() ?? throw new MissingComponentException("DirectionSwitch is not initialized!");
        }

        private void InitializeButtons()
        {
            buttonA = new ButtonA();
            buttonB = new ButtonB();
            buttonC = new ButtonC();

            if (buttonA == null || buttonB == null || buttonC == null)
            {
                throw new System.Exception("Button initialization failed. Ensure all buttons are properly instantiated.");
            }
        }

        private void InitializeStateMachine()
        {
            stateMachine = GetComponent<StateMachine>();

            if (stateMachine == null)
            {
                throw new System.Exception("StateMachine component is not available. This should not happen due to RequireComponent.");
            }
        }

        public MoveCommand GetMoveCommand()
        {
            if (groundSensor == null || directionMapper == null || directionSwitch == null)
            {
                Debug.LogWarning($"{nameof(InputListener)}: Missing components in GetMoveCommand.");
                return null;
            }

            if (!groundSensor.GetState()) return null;
            
            State currentPlayerState = stateMachine.GetCurrentState();

            if (currentPlayerState is AttackState) return null;

            var currentDirectionState = directionMapper.GetState();
            var isDoubleTap = doubleTapDetector?.Update(currentDirectionState) ?? false;

            if (directionSwitch.Update(currentDirectionState))
                return flipDirectionCommand;

            return currentDirectionState switch
            {
                DirectionMapper.State.Left or DirectionMapper.State.Right =>
                    isDoubleTap ? runCommand : walkCommand,

                DirectionMapper.State.Up or DirectionMapper.State.UpLeft or DirectionMapper.State.UpRight =>
                    jumpCommand,

                DirectionMapper.State.Down or DirectionMapper.State.DownLeft or DirectionMapper.State.DownRight =>
                    crouchCommand,

                _ => idleCommand
            };
        }

        public ButtonCommand GetButtonCommand()
        {
            if (buttonA == null || buttonB == null || buttonC == null)
            {
                Debug.LogWarning($"{nameof(InputListener)}: Buttons are not initialized.");
                return null;
            }

            if (buttonA.IsPressed()) return punchCommand;
            if (buttonB.IsPressed()) return kickCommand;

            // Explicitly handle ButtonC
            if (buttonC.IsPressed())
            {
                Debug.Log($"{nameof(InputListener)}: ButtonC pressed, but no action assigned.");
                return null;
            }

            return null;
        }

        public SensorCommand GetSensorCommand()
        {
            if (fallSensor == null)
            {
                Debug.LogWarning($"{nameof(InputListener)}: FallSensor is not assigned.");
                return null;
            }

            return fallSensor.GetState() ? fallCommand : null;
        }
    }
}
