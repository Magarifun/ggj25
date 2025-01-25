using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class DetectProximity : Rule
{
    public float range = 1f;
    public float timeThreshold = 0.5f;
    public bool addNoise = true;

    private Collider2D trigger;
    private readonly float checkPeriod = 0.1f;
    private int checkCount;
    private int detectionStreak = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (addNoise)
        {
            timeThreshold += Random.Range(-timeThreshold / 2, timeThreshold / 2);
        }
        checkCount = Mathf.RoundToInt(timeThreshold / checkPeriod);
        if (trigger == null)
        {
            trigger = gameObject.AddComponent<CircleCollider2D>();
            (trigger as CircleCollider2D).radius = range;
            trigger.isTrigger = true;
        }
        InvokeRepeating(nameof(CheckProximity), Random.Range(0, checkPeriod), checkPeriod);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void CheckProximity()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider in colliders)
        {
            if (IsValidTarget(collider.gameObject))
            {
                detectionStreak++;
                if (detectionStreak >= checkCount)
                {
                    detectionStreak = 0;
                    ApplyConsequences();
                }
                return;
            }
        }
        detectionStreak = 0;
    }
}
