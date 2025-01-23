using UnityEngine;

namespace egmp7.Game.Combat
{
    public class Attack
    {
        public Vector2 offset;
        public float radius;
        public float amount;
        public string from;
        public string to;

        // Constructor to initialize fields
        public Attack(Vector2 offset, float radius, float amount, string from, string to)
        {
            this.offset = offset;
            this.radius = radius;
            this.amount = amount;
            this.from = from;
            this.to = to;
        }
    }
}
