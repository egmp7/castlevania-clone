using UnityEngine;

namespace egmp7.SO
{
    [CreateAssetMenu(fileName = "LockFSM", menuName = "egmp7/LockFSM", order = 1)]
    public class LockFSM : ScriptableObject
    {
        [Header("FSM Lock Setup")]

        public float LockTime;
        public string[] FSMsToLock;
    }
}