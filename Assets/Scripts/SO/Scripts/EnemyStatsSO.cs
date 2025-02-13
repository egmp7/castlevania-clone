using egmp7.Types;
using UnityEngine;

namespace egmp7.SO
{
    [CreateAssetMenu(fileName = "EnemyStatsSO", menuName = "egmp7/EnemyStatsSO", order = 1)]
    public class EnemyStatsSO : ScriptableObject
    {
        [Header("Enemy Stats")]
        public int points;
        public int punchCount;
        public int kickCount;
        public int specialCount;
        public int blockCount;
    }
}