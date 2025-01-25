using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    // Method to reload the current active scene
    public void ReloadCurrentScene()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();
        
        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }
}