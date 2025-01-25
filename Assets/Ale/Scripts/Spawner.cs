using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Array of objects to spawn
    public GameObject[] objectsToSpawn;

    // Default spawn area boundaries
    public Vector2 spawnAreaMin = new Vector2(-5, -5);
    public Vector2 spawnAreaMax = new Vector2(5, 5);

    public void SpawnObject(int index, int burstCount)
    {
        if (objectsToSpawn == null || objectsToSpawn.Length == 0)
        {
            Debug.LogWarning("No objects set to spawn!");
            return;
        }

        if (index < 0 || index >= objectsToSpawn.Length)
        {
            Debug.LogError($"Invalid index {index}. Index must be between 0 and {objectsToSpawn.Length - 1}.");
            return;
        }

        for (int i = 0; i < burstCount; i++)
        {
            // Random position within the spawn area
            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            // Spawn the object
            Instantiate(objectsToSpawn[index], spawnPosition, Quaternion.identity);
        }
    }
}
