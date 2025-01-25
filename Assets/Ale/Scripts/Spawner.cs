using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;

    public int burstCount = 1;

    public Vector2 spawnAreaMin = new Vector2(-5, -5);
    public Vector2 spawnAreaMax = new Vector2(5, 5);

    public Color gizmoColor = new Color(0, 1, 0, 0.3f); // Light green color with transparency

    public void SpawnObject(int index, int count)
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

        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            Instantiate(objectsToSpawn[index], spawnPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        // Calculate the center and size of the spawn area
        Vector2 center = (spawnAreaMin + spawnAreaMax) / 2;
        Vector2 size = spawnAreaMax - spawnAreaMin;

        // Draw the spawn area as a transparent cube (2D: flat rectangle)
        Gizmos.DrawCube(center, size);

        // Optional: Draw the edges of the spawn area for clarity
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }
}
