using HutongGames.PlayMaker;
using InputCommands.Move;

namespace egmp7.Playmaker.Actions
{
    [ActionCategory("Input")]
    [Tooltip("Gets the current direction from the input")]
    public class GetCurrentDirection : FsmStateAction
    {
        [Tooltip("Current move direction")]
        public FsmFloat direction;

        [Tooltip("Event sent when direction is equal to 0")]
        public FsmEvent onIdle;

        [Tooltip("Event sent when direction is different to 0")]
        public FsmEvent onMove;

        private DirectionMapper _directionMapper;

        public override void Reset()
        {
            direction = 0f;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _directionMapper = new DirectionMapper();
        }

        public override void OnUpdate()
        {

            var state  = _directionMapper.GetState();
            if (
                state == DirectionMapper.State.UpLeft ||
                state == DirectionMapper.State.DownLeft ||
                state == DirectionMapper.State.Left)
            {
                direction.Value = -1f;
                Fsm.Event(onMove);
            }
            else if(
                state == DirectionMapper.State.UpRight ||
                state == DirectionMapper.State.DownRight ||
                state == DirectionMapper.State.Right) 
            { 
                direction.Value = 1f;
                Fsm.Event(onMove);
            }
            else
            {
                direction.Value = 0f;
                Fsm.Event(onIdle);
            }
        }
    }
}
