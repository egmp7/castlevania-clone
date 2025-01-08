using Game.AnimationEvent.Receiver;
using System;
using UnityEngine;

namespace Game.AnimationEvent.Source
{
    /// <summary>
    /// Sends damage to Damage Receiver Object 
    /// </summary>
    public class DamageProcessor : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform attackTransform;

        [Header("Attack Parameters")]
        [HideInInspector] public Vector2 offset;
        [HideInInspector] public float attackRadius;
        [HideInInspector] public float currentDamage;

        private void Awake()
        {
            // Validate required components
            if (attackTransform == null)
            {
                Debug.LogError($"{nameof(attackTransform)} is not assigned on {name}. Disabling script.");
                enabled = false;
            }
        }

        public void PerformAttackDetection()
        {
            if (!enabled)
            {
                Debug.LogWarning("Cannot perform attack detection; script is disabled.");
                return;
            }

            if (attackRadius <= 0)
            {
                Debug.LogWarning($"Attack radius is not set or is invalid (value: {attackRadius}). Defaulting to 1.");
                attackRadius = 1f;
            }

            if (currentDamage <= 0)
            {
                Debug.LogWarning($"Current damage is not set or is invalid (value: {currentDamage}). Defaulting to 10.");
                currentDamage = 10f;
            }

            // Calculate attack position
            Vector2 attackPosition = FindAttackPosition(
                attackTransform.position,
                offset,
                (int)Mathf.Sign(transform.localScale.x));

            // Draw debug circle
            Utilities.DrawCircleBounds(
                attackPosition,
                attackRadius,
                Color.cyan);

            // Detect hit
            Collider2D hit = Physics2D.OverlapCircle(
                attackPosition,
                attackRadius,
                layerMask);

            // Handle hit detection
            if (hit != null)
            {
                if (hit.TryGetComponent<DamageListener>(out var damageListener))
                {
                    damageListener.TakeDamage(currentDamage, layerMask);
                }
                else
                {
                    Debug.LogWarning($"Hit object does not have a {nameof(DamageListener)} component. Object: {hit.name}");
                }
            }
            else
            {
                Debug.Log("No hit during attack detection.");
            }
        }

        /// <summary>
        /// Calculates the attack position based on a given position, offset, and direction.
        /// </summary>
        /// <param name="position">The base position.</param>
        /// <param name="offset">The offset to apply to the base position.</param>
        /// <param name="direction">The direction multiplier, typically -1 (left) or 1 (right).</param>
        /// <returns>A Vector2 representing the calculated attack position.</returns>
        /// <exception cref="ArgumentNullException">Thrown if position or offset is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the direction is not -1 or 1.</exception>
        private Vector2 FindAttackPosition(Vector2? position, Vector2? offset, int direction)
        {
            // Validate position
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position), "Position cannot be null.");
            }

            // Validate offset
            if (offset == null)
            {
                throw new ArgumentNullException(nameof(offset), "Offset cannot be null.");
            }

            // Validate direction
            if (direction != -1 && direction != 1)
            {
                throw new ArgumentException("Direction must be either -1 (left) or 1 (right).", nameof(direction));
            }

            // Compute attack position
            return new Vector2(
                position.Value.x + (offset.Value.x * direction),
                position.Value.y + offset.Value.y
            );
        }
    }
}

