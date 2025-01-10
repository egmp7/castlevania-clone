using egmp7.Game.Combat;
using System;
using UnityEngine;
using UnityEngine.Events;

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

        [Header("Attacked received event")]
        [SerializeField] private UnityEvent UnityEvent;

        private Attack _fromAttack;
        private Attack _toAttack;

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
            var direction = (int)Mathf.Sign(transform.localScale.x);
            var attackInitPosition = attackTransform.position;

            // Calculate attack position
            Vector2 attackPosition = FindAttackPosition(
                attackInitPosition,
                _fromAttack.offset,
                direction);

            // Draw debug circle
            Utilities.DrawCircleBounds(
                attackPosition,
                _fromAttack.radius,
                Color.cyan);

            // Detect hit
            Collider2D hit = Physics2D.OverlapCircle(
                attackPosition,
                _fromAttack.radius,
                layerMask);

            // Handle hit detection
            if (hit != null)
            {
                if (hit.TryGetComponent<DamageProcessor>(out var processor))
                {
                    processor.SendAttack(_fromAttack);
                }
                else
                {
                    ErrorManager.LogMissingComponent<DamageProcessor>(hit);
                }
            }
            else
            {
                //Debug.Log("No hit during attack detection.");
            }
        }

        public void SetFromAttack(Attack attack)
        {
            _fromAttack = attack;
        }

        public void SendAttack(Attack attack)
        {
            Debug.Log($"Attacked received: {attack.amount}, From: {attack.from} To: {attack.to}");
            _toAttack = attack;
            UnityEvent.Invoke();
        }

        public Attack GetToAttack()
        {
            return _toAttack;
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
        private static Vector2 FindAttackPosition(Vector2? position, Vector2? offset, int direction)
        {
            #region Find Attack Position
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
            #endregion
        }
    }
}

