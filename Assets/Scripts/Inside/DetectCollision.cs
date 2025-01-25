using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class DetectCollision : Rule
{
    public bool combineTwoElements = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsValidTarget(collision.gameObject, noTargetMeansAll: true))
        {
            if (combineTwoElements)
            {
                // Only the older element applies the effect
                if (collision.gameObject.GetInstanceID() > gameObject.GetInstanceID())
                {
                    ApplyOwnConsequences();
                    return;
                }
            }
            ApplyConsequences();
        }
    }
}
