using Game.Sensors;
using UnityEngine;

namespace egmp7.Game.Settings
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Game/Player Settings", order = 1)]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Player Stats")]
        public float maxHealth = 500f;
        public float jumpForce = 10f;
        public float direction = 0f;
        [Range(1.0f, 50.0f)] public float walkSpeed = 10.0f;
        [Range(1.0f, 50.0f)] public float runSpeed = 20.0f;
        [Range(0.1f, 10.0f)] public float accelerationRate = 5f;
        public float blockDamageReducer = 0.1f;

        [Header("Attack")]
        public LayerMask enemyLayer;
        public Transform EnemyDetectorPosition;
        [Tooltip("Reset time of the punch combo")]
        [Range(0.0f, 2f)] public float punchComboResetTime = 1f;
        [Tooltip("Reset time of the punch combo")]
        [Range(0.0f, 2f)] public float kickComboResetTime = 0.8f;
        [Tooltip("Reset time of the punch combo")]
        public Vector2 attackOffset;

        [Header("Attack Settings")]
        public LayerMask layerMask;
        public Transform attackTransform;

        [Header("Sensors")]
        public GroundSensor groundSensor;
        public FallSensor fallSensor;

        [Header("Abilities")]
        public bool canDoubleJump = false;
        public bool canDash = false;

        [Header("Combat")]
        public float attackDamage = 25f;
        public float attackSpeed = 1.2f;

        [Header("Combo Settings")]
        public int maxCombo = 3;
        public float comboResetTime = 2f;

    }
}