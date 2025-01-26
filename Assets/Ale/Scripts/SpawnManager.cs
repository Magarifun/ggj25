using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SpawnEvent : UnityEvent<int, int> { } // Custom event with two arguments: index and burstCount

public class SpawnManager : MonoBehaviour
{
    public SpawnEvent onSpawn; // Exposed UnityEvent for spawning
    public Spawner spawner;   // Reference to the Spawner script
    public bool restoreAfterFastForward = false;

    private string lastSpawnArgs = ""; // Store the last arguments used to spawn

    private void Start()
    {
        if (onSpawn == null)
            onSpawn = new SpawnEvent();

        // Subscribe the Spawner's method to the event
        onSpawn.AddListener(HandleSpawnEvent);
    }

    private void HandleSpawnEvent(int index, int burstCount)
    {
        if (spawner != null)
        {
            spawner.SpawnObject(index, burstCount);
        }
        else
        {
            Debug.LogError("Spawner not assigned in SpawnManager!");
        }
    }

    // Helper method to trigger the event from a UI button
    public void TriggerSpawnWithArguments(string args)
    {
        lastSpawnArgs = args;

        if (args.StartsWith("!"))
        {
            TriggerSpecialEvent(args.Substring(1));
            return;
        }

        // Parse the input string into two arguments
        string[] splitArgs = args.Split(',');
        if (splitArgs.Length != 2)
        {
            Debug.LogError("Invalid arguments! Provide input in the format 'index,burstCount'.");
            return;
        }

        if (int.TryParse(splitArgs[0], out int index) && int.TryParse(splitArgs[1], out int burstCount))
        {
            onSpawn.Invoke(index, burstCount);
        }
        else
        {
            Debug.LogError("Invalid arguments! Both index and burstCount must be integers.");
        }
    }

    public void RepeatLastEvent() => TriggerSpawnWithArguments(lastSpawnArgs);

    private void TriggerSpecialEvent(string eventName)
    {
        switch (eventName)
        {
            case "fastforward":
                if (restoreAfterFastForward)
                {
                    Invoke(nameof(RestoreNormalTimeScale), 60f);
                }
                Time.timeScale *= 2;
                break;

            case "earthquake":
                FindFirstObjectByType<WorldShaker>().Quake("earthquakeable", 3);
                break;

            case "tide":
                FindFirstObjectByType<WorldShaker>().Tide();
                break;

            case "hurricane":
                FindFirstObjectByType<WorldShaker>().Quake("gas", 2);
                break;

        }
    }

    private void RestoreNormalTimeScale()
    {
        Time.timeScale /= 2;
    }
}
