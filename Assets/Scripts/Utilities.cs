using UnityEngine;

public class Utilities : MonoBehaviour
{
    static public void DebugDrawBoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, Color color)
    {
        Vector2 castEndPoint = origin + direction * distance;
        Vector2 rotatedSize = Quaternion.Euler(0, 0, angle) * size;

        // Calculate the four corners of the box
        Vector2 upperLeft = origin - rotatedSize * 0.5f;
        Vector2 upperRight = origin + new Vector2(rotatedSize.x * 0.5f, -rotatedSize.y * 0.5f);
        Vector2 lowerLeft = origin + new Vector2(-rotatedSize.x * 0.5f, rotatedSize.y * 0.5f);
        Vector2 lowerRight = origin + rotatedSize * 0.5f;

        // Draw the box cast using Debug.DrawLine for each side of the box
        Debug.DrawLine(upperLeft, upperRight, color);
        Debug.DrawLine(upperRight, lowerRight, color);
        Debug.DrawLine(lowerRight, lowerLeft, color);
        Debug.DrawLine(lowerLeft, upperLeft, color);

        // Draw the direction line
        Debug.DrawRay(origin, direction * distance, color);
    }

}
