using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class DetectProximity : TimedRule
{
    public float range = 1f;
    private Collider2D trigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (trigger == null)
        {
            trigger = gameObject.AddComponent<CircleCollider2D>();
            (trigger as CircleCollider2D).radius = range;
            trigger.isTrigger = true;
        }
        StartPeriodicChecks();
    }

    protected override bool CheckIfConditionPersists()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider in colliders)
        {
            if (IsValidTarget(collider.gameObject))
            {
                return true;
            }
        }
        return false;
    }
}
