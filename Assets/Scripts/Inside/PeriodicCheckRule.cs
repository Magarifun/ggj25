using UnityEngine;

public abstract class PeriodicCheckRule : Rule
{
    protected readonly float checkPeriod = 0.1f;

    protected virtual void StartPeriodicChecks()
    {
        InvokeRepeating(nameof(Check), Random.Range(0, checkPeriod), checkPeriod);
    }

    protected abstract void Check();
}
