using UnityEngine;
using HutongGames.PlayMaker; // Import PlayMaker namespace

namespace egmp7.Playmaker.Actions
{

    [ActionCategory("Custom")] // Organizes this action in PlayMaker's action menu
    [HutongGames.PlayMaker.Tooltip("Detects single and double taps using internal checks.")]
    public class DoubleTapDetectorAction : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("Time threshold for detecting a double tap.")]
        public FsmFloat doubleTapThreshold;

        [HutongGames.PlayMaker.Tooltip("Event to send if a single tap is detected.")]
        public FsmEvent singleTapEvent;

        [HutongGames.PlayMaker.Tooltip("Event to send if a double tap is detected.")]
        public FsmEvent doubleTapEvent;

        private float lastTapTime;
        private bool isTapCheck;

        public override void Reset()
        {
            doubleTapThreshold = 0.3f; // Default threshold value
            singleTapEvent = null;
            doubleTapEvent = null;
            lastTapTime = 0;
            isTapCheck = false;
        }

        public override void OnUpdate()
        {
            DetectTap();
        }

        private void DetectTap()
        {
            if (!isTapCheck)
            {
                isTapCheck = true;

                // Check for double tap
                if (Time.time - lastTapTime < doubleTapThreshold.Value)
                {
                    Fsm.Event(doubleTapEvent);
                }
                else
                {
                    Fsm.Event(singleTapEvent);
                }

                lastTapTime = Time.time;
            }
            else
            {
                isTapCheck = false;
            }
        }
    }
}
