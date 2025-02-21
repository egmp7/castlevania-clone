using egmp7.Types;
using UnityEngine;

namespace egmp7.SO
{
    [CreateAssetMenu(fileName = "AttackSO", menuName = "egmp7/AttackSO", order = 1)]
    public class AttackSO : ScriptableObject
    {
        [Header("Attack Main")]
        public int damage;
        public AttackType attackType;
        
        [Header("Only for Player")]
        public string animationName;
        public float tapMaxTime;
        public float tapMinTime;

        [Header("Only for Enemy")]
        [Range(0, 1)] public float blockChance;
    }
}