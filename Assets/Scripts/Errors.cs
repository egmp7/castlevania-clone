using UnityEngine;

public static class ErrorManager
{
    /// <summary>
    /// Generic warning for missing components
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    public static void LogMissingComponent<T>(GameObject gameObject) where T : Component
    {
        Debug.LogWarning($"{gameObject.name} game object does not have a {typeof(T).Name} component.");
    }

    /// <summary>
    /// Logs a warning when a Collider2D is missing an expected component
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collider"></param>
    public static void LogMissingComponent<T>(Collider2D collider) where T : Component
    {
        if (collider == null)
        {
            Debug.LogWarning($"The provided Collider2D is null. Cannot check for missing components.");
            return;
        }

        Debug.LogWarning($"Collider2D object '{collider.name}' does not have a {typeof(T).Name} component.");
    }


    /// <summary>
    /// Logs a warning when no GameObject is found with the specified tag.
    /// </summary>
    /// <param name="tag">The tag used to search for the GameObject.</param>
    public static void LogMissingGameObjectWithTag(string tag)
    {
        Debug.LogWarning($"No GameObject found with tag: {tag}");
    }



    // Generic error for uninitialized values
    public static void LogUninitializedValue(string valueName, string context = "")
    {
        string message = $"The value '{valueName}' is not initialized.";
        if (!string.IsNullOrEmpty(context))
        {
            message += $" Context: {context}.";
        }
        Debug.LogError(message);
    }


    // Custom warning or error messages
    public static void LogCustomWarning(string message)
    {
        Debug.LogWarning(message);
    }

    public static void LogCustomError(string message)
    {
        Debug.LogError(message);
    }
}
