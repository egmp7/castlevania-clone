public class FallState : AirState
{
    protected override void OnEnter()
    {
        base.OnEnter();
        input.animationController.PlayFallAnimation();
    }
}
