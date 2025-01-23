using UnityEngine;

namespace egmp7.Game.Test
{

    public class Collider2DTest : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.name);
        }
    }
}

