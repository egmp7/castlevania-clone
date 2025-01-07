using UnityEngine;
using UnityEngine.Events;
namespace Game.AnimationEvent.Receiver
{
    public class DamageListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent UnityEvent;

        public void TakeDamage(float damage, LayerMask layerMask)
        {
            // Get the name of the layer(s) from the layer mask
            string layerNames = GetLayerNames(layerMask);

            // Log the layer names and damage received
            Debug.Log($"Damage received: {damage}, Layer(s): {layerNames}");

            UnityEvent.Invoke();
        }

        private string GetLayerNames(LayerMask layerMask)
        {
            string names = "";

            // Loop through all layers to find the active ones in the layer mask
            for (int i = 0; i < 32; i++)
            {
                // Check if the layer is included in the layer mask
                if ((layerMask & (1 << i)) != 0)
                {
                    names += LayerMask.LayerToName(i) + " "; // Get the layer name
                }
            }

            return names.Trim(); // Trim any extra spaces
        }
    }
}
