using UnityEngine;
using HutongGames.PlayMaker;
namespace egmp7.Playmaker.Actions
{

    [ActionCategory("Physics2D")]
    public class RBJump : FsmPlayerAction
    {
        public override void OnEnter()
        {
            base.OnEnter();
            // jump
            rb.velocity = new Vector2(rb.velocity.x, playerSettings.jumpForce);
        }
    }
}
