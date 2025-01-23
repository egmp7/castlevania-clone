using UnityEngine;
using HutongGames.PlayMaker;
namespace egmp7.Playmaker.Actions
{

    [ActionCategory("Physics2D")]
    public class RBMove : FsmPlayerAction
    {

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The direction of movement (-1 for left, 1 for right, 0 for no movement).")]
        public FsmFloat direction;

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("Changes the speed if running is true")]
        public FsmBool running;

        public override void Reset()
        {
            base.Reset();
            direction = 0f;
        }

        public override void OnUpdate()
        {
            // Stop horizontal movement if direction is 0
            if (Mathf.Approximately(direction.Value, 0))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                return;
            }

            // Calculate current speed using the provided algorithm
            float currentSpeed = CalculateCurrentSpeed(
                rb.velocity.x,
                direction.Value,
                running.Value ? playerSettings.runSpeed : playerSettings.walkSpeed,
                playerSettings.accelerationRate
            );

            // Preserve vertical velocity and apply the calculated horizontal speed
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        }

        private float CalculateCurrentSpeed(float currentVelocityX, float direction, float walkSpeed, float accelerationRate)
        {
            return Mathf.Lerp(currentVelocityX, direction * walkSpeed, accelerationRate * Time.deltaTime);
        }
    }
}
