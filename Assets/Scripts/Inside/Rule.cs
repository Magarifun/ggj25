using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Element))]
public class Rule : MonoBehaviour
{
    public Element[] replaceWith;
    public Element[] spawn;
    public string[] targetElementTags;

    public void ApplyConsequences()
    {
        if (replaceWith.Length > 0)
        {
            Destroy(this.gameObject);
            foreach (Element replacement in replaceWith)
            {
                var newElement = Instantiate(replacement, transform.position, Quaternion.identity, transform.parent);
                newElement.name = $"{replacement.name} <- {this.gameObject.name}";
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
