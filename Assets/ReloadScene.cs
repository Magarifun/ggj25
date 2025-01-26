using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneReloader : MonoBehaviour
{
    public GameObject[] objectsToDisable; // Array of game objects to disable during the delay
    public float reloadDelay = 2.0f; // Delay before reloading the scene

    // Method to reload the current active scene with a delay
    public void ReloadCurrentSceneWithDelay()
    {
        Time.timeScale = 1;
        Element.ResetCounters();
        StartCoroutine(ReloadSceneAfterDelay());
    }

    private IEnumerator ReloadSceneAfterDelay()
    {
        // Disable all the specified game objects
        foreach (var obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        // Wait for the specified delay
        yield return new WaitForSeconds(reloadDelay);

        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }
}
