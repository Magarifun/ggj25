using UnityEngine;

public class TimedRule : PeriodicCheckRule
{
    [Header("Time threshold")]
    public float timeThreshold = 0.5f;
    public bool addNoise = true;

    private int checkCount;
    private int detectionStreak = 0;

    private void Start()
    {
        StartPeriodicChecks();
    }

    protected override void StartPeriodicChecks()
    {
        if (addNoise)
        {
            timeThreshold += Random.Range(-timeThreshold / 2, timeThreshold / 2);
        }
        checkCount = Mathf.RoundToInt(timeThreshold / checkPeriod);
        if (checkCount < 1)
        {
            checkCount = 1;
        }
        base.StartPeriodicChecks();
    }

    protected sealed override void Check()
    {
        bool persists = CheckIfConditionPersists();

        if (persists)
        {
            detectionStreak++;
            if (detectionStreak >= checkCount)
            {
                detectionStreak = 0;
                ApplyConsequences();
            }
        }
        else
        {
            detectionStreak = 0;
        }
    }

    protected virtual bool CheckIfConditionPersists() => true;
}
