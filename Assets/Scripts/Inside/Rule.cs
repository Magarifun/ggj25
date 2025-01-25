using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Element))]
public class Rule : MonoBehaviour
{
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
                // TODO if (!destroy) push newly spawned object
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
