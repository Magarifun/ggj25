using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Element))]
public class Rule : MonoBehaviour
{
    public const float SPAWN_IMPULSE = 1;

    public string[] targetElementTags;
    public string[] ignoreElementTags;

    public bool destroy = false;
    public Element[] spawn;
    public bool upgrade = false;

    protected virtual void ApplyOwnConsequences()
    {
        Element element = GetComponent<Element>();
        if (destroy)
        {
            Destroy(this.gameObject);
        }
        else if (upgrade)
        {
            element.Upgrade();
        }
    }

    public void ApplyConsequences()
    {
        ApplyOwnConsequences();
        
        if (spawn.Length > 0)
        {
            foreach (Element replacement in spawn)
            {
                var newElement = Instantiate(replacement, transform.position, Quaternion.identity, transform.parent);
                newElement.name = $"{replacement.name} <- {this.gameObject.name}";

                if (destroy)
                {
                    Rigidbody2D ownBody = newElement.GetComponent<Rigidbody2D>();
                    Rigidbody2D otherBody = newElement.GetComponent<Rigidbody2D>();
                    otherBody.linearVelocity = ownBody.linearVelocity;
                    otherBody.angularVelocity = ownBody.angularVelocity;
                }
                else
                {
                    // Throws newly spawned object if it's not a replacement rule
                    Vector2 randomOffset = (Random.insideUnitCircle * 0.1f);
                    if (transform.localPosition.y < 0 && randomOffset.y < 0)
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
        bool targettable = false;
        if (target.CompareTag("Element"))
        {
            if (target.TryGetComponent<Element>(out Element element))
            {
                if (element.UsedInRule)
                {
                    return false;
                }
                foreach (string elementTag in element.elementTags)
                {
                    if (ignoreElementTags.Contains(elementTag))
                    {
                        return false;
                    }
                    if (targetElementTags.Length == 0)
                    {
                        targettable = noTargetMeansAll;
                    }
                    else if (targetElementTags.Contains(elementTag))
                    {
                        targettable = true;
                    }
                }
                if (targettable)
                {
                    element.MarkAsUsedInRule();
                }
            }
        }
        return targettable;
    }
}
