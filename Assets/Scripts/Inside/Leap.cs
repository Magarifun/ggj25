using Unity.VisualScripting;
using UnityEngine;

public class Leap : TimedRule
{
    public float leapForce = 1f;

    protected override void ApplyOwnConsequences()
    {
        base.ApplyOwnConsequences();
        Vector2 direction = Random.Range(0, 2) == 0 ? Vector2.left : Vector2.right;
        direction += Vector2.up;
        direction.Normalize();
        GetComponent<Rigidbody2D>().AddForce(direction * leapForce, ForceMode2D.Impulse);
    }

}
