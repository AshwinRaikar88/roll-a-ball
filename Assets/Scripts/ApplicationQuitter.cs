using UnityEngine;
// Required for Editor-specific functions
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ApplicationQuitter : MonoBehaviour
{
    // This public method can be called by the UI Button
    public void QuitGame()
    {
        Debug.Log("Quit button clicked!"); // Optional: for confirmation

        // Conditional compilation block
        #if UNITY_EDITOR
        // If running in the Unity Editor
        EditorApplication.isPlaying = false;
        Debug.Log("Stopped Editor Play Mode.");
        #else
        // If running in a built application
        Application.Quit();
        Debug.Log("Application Quit requested.");
        // Note: Application.Quit() might be ignored on some platforms like WebGL
        // or might not happen immediately on mobile platforms.
        #endif
    }
}