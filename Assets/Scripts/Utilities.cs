using System;
using UnityEngine;

public class Utilities
{

    /// <summary>
    /// Draws cross lines to visualize the bounds of a circle in 2D space.
    /// </summary>
    /// <param name="center">The center position of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="color">The color of the debug lines.</param>
    /// <param name="duration">The duration the lines will be visible.</param>
    public static void DrawCircleBounds(Vector3 center, float radius, Color color, float duration = 0.1f)
    {
        // Horizontal line
        Debug.DrawLine(center + Vector3.right * radius, center - Vector3.right * radius, color, duration);

        // Vertical line
        Debug.DrawLine(center + Vector3.up * radius, center - Vector3.up * radius, color, duration);
    }

    static public float CalculateCurrentSpeed(
        float currentXVelocity,
        int direction,
        float speed,
        float accelerationRate)
    {
        // Validate direction
        if (direction != -1 && direction != 0 && direction != 1)
        {
            throw new ArgumentOutOfRangeException(nameof(direction), "Direction must be -1, 0, or 1.");
        }

        // Validate speed
        if (speed < 0f)
        {
            throw new ArgumentOutOfRangeException(nameof(speed), "Speed cannot be negative.");
        }

        // Validate accelerationRate
        if (accelerationRate < 0f || accelerationRate > 50f)
        {
            throw new ArgumentOutOfRangeException(nameof(accelerationRate), "Acceleration rate must be between 0 and 50.");
        }

        // Ensure Time.fixedDeltaTime is non-zero
        if (Time.fixedDeltaTime <= 0f)
        {
            throw new InvalidOperationException("Time.fixedDeltaTime must be greater than 0.");
        }

        // Calculate the interpolated speed
        return Mathf.Lerp(
            currentXVelocity,
            direction * speed,
            accelerationRate * Time.fixedDeltaTime);
    }

    static public void DebugDrawBoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, Color color)
    {
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

    public static void DrawCircle(Vector3 position, float radius, int segments = 32)
    {
        float angleStep = 360f / segments;
        Vector3 lastPoint = position + new Vector3(radius, 0, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 newPoint = position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            Debug.DrawLine(lastPoint, newPoint, Color.red); // Draws in the Scene view during Play mode
            lastPoint = newPoint;
        }
    }

    /// <summary>
    /// // Get the name of the layer(s) from the layer mask
    /// </summary>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    public static string GetLayerNames(LayerMask layerMask)
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

    /// <summary>
    /// Destroys all game objects with the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the game objects to destroy.</param>
    public static void DestroyAllWithTag(string tag)
    {
        // Find all game objects with the specified tag
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tag);

        // Destroy each game object
        foreach (GameObject obj in objectsToDestroy)
        {
            UnityEngine.Object.Destroy(obj);
        }
    }

}
