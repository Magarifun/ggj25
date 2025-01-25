using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Element))]
public class Rule : MonoBehaviour
{
    public const float SPAWN_IMPULSE = 1;

    public string[] targetElementTags;
    public bool destroy = false;
    public Element[] spawn;

    public void ApplyConsequences()
    {
        if (destroy)
        {
            Destroy(this.gameObject);
        }
        if (spawn.Length > 0)
        {
            foreach (Element replacement in spawn)
            {
                var newElement = Instantiate(replacement, transform.position, Quaternion.identity, transform.parent);
                newElement.name = $"{replacement.name} <- {this.gameObject.name}";

                // Throws away newly spawned object if it's not a replacement rule
                if (!destroy)
                {
                    Vector2 randomOffset = (Random.insideUnitCircle * 0.1f);
                    if (randomOffset.y < 0)
                    {
                        randomOffset.y = -randomOffset.y;
                    }
                    newElement.transform.position += (Vector3) randomOffset;
                    if (newElement.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                    {
                        rb.AddForce(randomOffset * SPAWN_IMPULSE, mode: ForceMode2D.Impulse);
                    }
                }
            }
        }
    }

    protected bool IsValidTarget(GameObject target, bool noTargetMeansAll = false)
    {   
        if (target.CompareTag("Element"))
        {
            if (targetElementTags.Length == 0)
            {
                return noTargetMeansAll;
            }
            if (target.TryGetComponent<Element>(out Element element))
            {
                if (element.elementTags.Any(targetElementTags.Contains))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
