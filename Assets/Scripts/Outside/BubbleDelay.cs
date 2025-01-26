using UnityEngine;

public class BubbleDelay : MonoBehaviour
{
    public float overallDelay;
    private ElementSpawner[] spawners;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawners = GetComponentsInChildren<ElementSpawner>();
        foreach (ElementSpawner spawner in spawners)
        {
            spawner.enabled = false;
        }
        Invoke(nameof(EnableSpawners), overallDelay);
    }

    void EnableSpawners()
    {
        foreach (ElementSpawner spawner in spawners)
        {
            spawner.enabled = true;
        }
        Destroy(this);
    }
}
