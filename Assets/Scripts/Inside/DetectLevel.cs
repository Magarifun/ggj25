using JetBrains.Annotations;
using System.Linq;
using UnityEngine;

public class DetectLevel : TimedRule
{
    public const int DEEP_THRESHOLD = 5;

    private readonly float considerUpwardRange = 5f;
    private readonly float considerDownwardRange = 2f;

    private Level level = Level.Floating;

    public enum Level
    {
        Floating, // is also Surface
        Surface,
        Buried,
        Deep // is also Buried
    }

    public Level targetLevel = Level.Buried;
    public bool strict = false; // if false, Deep -> Buried and Surface -> Floating

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartPeriodicChecks();
    }

    protected override bool CheckIfConditionPersists()
    {
        int countUp = CountInDirection(Vector2.up, considerUpwardRange);
        int countDown = CountInDirection(Vector2.down, considerDownwardRange);
        if (countUp == 0)
        {
            level = countDown == 0 ? Level.Floating : Level.Surface;
        }
        else if (countUp == 1)
        {
            level = Level.Surface;
        }
        else if (countUp < DEEP_THRESHOLD)
        {
            level = Level.Buried;

        }
        else
        {
            level = Level.Deep;
        }

        if (level == targetLevel)
        {
            return true;
        } 
        else if (!strict)
        {
            if (level == Level.Floating && targetLevel == Level.Surface)
            {
                return true;
            } 
            else if (level == Level.Deep && targetLevel == Level.Buried)
            {
                return true;
            }
        }
        return false;
    }

    private int CountInDirection(Vector2 direction, float distance)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, distance);
        if (direction == Vector2.up) {
            Debug.DrawRay(transform.position, direction * distance, Color.red);
        }
        return hits.Count(hit => !hit.collider.isTrigger && IsValidTarget(hit.collider.gameObject, noTargetMeansAll: true)) - 1;
    }
}
