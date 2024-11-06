using UnityEngine;

public class ZoneSensor : MonoBehaviour
{
    [HideInInspector] public bool ActiveZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActiveZone = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActiveZone = false;
        }
    }
}
