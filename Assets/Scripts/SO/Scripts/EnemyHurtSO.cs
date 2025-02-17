using UnityEngine;

namespace egmp7.SO
{
    [CreateAssetMenu(fileName = "EnemyHurt", menuName = "egmp7/EnemyHurt", order = 1)]
    public class EnemyHurtSO : ScriptableObject
    {
        public string animation;
        public float minDistance;
        public float maxDistance;
        public float baseForceMagnitud;
        public float maxMultiplier;
    }
}
