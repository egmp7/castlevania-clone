using UnityEngine;

namespace egmp7.Game.Enemies
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        public BaseEnemy Copy(Transform transform)
        {
            BaseEnemy clone = Instantiate(this);
            clone.transform.localPosition = GenerateRandomPosition() + transform.position;

            return clone;
        }

        private static Vector3 GenerateRandomPosition()
        {
            return new Vector3(Random.Range(-3, 3), 0, 0);
        }
    }

}

