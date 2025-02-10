using UnityEngine;

namespace egmp7.SO
{
    [CreateAssetMenu(fileName = "GameManager", menuName = "egmp7/GameManager", order = 1)]
    public class GameManagerSO : ScriptableObject
    {
        [Header("Game Manager")]
        public int score;
        public int round;
        public int enemiesDestroyed;
        public int currentHP;
        public int maxHP;
    }
}