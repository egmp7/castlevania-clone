using Game.AnimationEvent.Source;
using UnityEngine;

namespace egmp7.Game.Combat
{

    public class Weapon : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<DamageProcessor>(out var processor))
            {
                //processor.SendAttack(_fromAttack);
            }
        }
    }
}

