using UnityEngine;
using HutongGames.PlayMaker;
using egmp7.SO;

namespace egmp7.Playmaker.Actions
{

    [ActionCategory("Physics2D")]
    public abstract class FsmPlayerAction : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(Rigidbody2D))]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The ScriptableObject containing speed settings.")]
        public PlayerSettings playerSettings;

        [HutongGames.PlayMaker.Tooltip("The Rigidbody2D component of the object.")]
        protected Rigidbody2D rb;

        public override void Reset()
        {
            gameObject = null;
            playerSettings = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go != null)
            {
                rb = go.GetComponent<Rigidbody2D>();
            }

            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component is missing on the target object.");
                Finish();
            }

            if (playerSettings == null)
            {
                Debug.LogError("Player Settings is null, please add a Player Settings Instance");
                Finish();
            }
        }
    }
}
