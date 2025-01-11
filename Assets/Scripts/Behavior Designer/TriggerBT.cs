using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Enemy.AI
{

    public class TriggerBT : MonoBehaviour
    {
        public void TriggerHurtBT()
        {
            // Trigger the event in Behavior Designer
            BehaviorManager.instance.SendMessage("OnHurt");
        }
    }
}
