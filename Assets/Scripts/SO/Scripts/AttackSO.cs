using egmp7.Types;
using UnityEngine;

namespace egmp7.SO
{
    [CreateAssetMenu(fileName = "PunchAttack", menuName = "egmp7/PunchAttack", order = 1)]
    public class AttackSO : ScriptableObject
    {
        [Header("Punch attack")]
        public int damage;
        public AttackType attackType;
        public string animationName;
        public float tapMaxTime;
        public float tapMinTime;
    }
}