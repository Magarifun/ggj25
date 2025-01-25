using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class DetectCollision : Rule
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsValidTarget(collision.gameObject))
        {
            ApplyConsequences();
        }
    }
}
