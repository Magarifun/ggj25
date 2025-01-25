using UnityEngine;

public class ReplaceElement : Consequence
{
    protected void Replace(Element replaceWith)
    {
        Destroy(this.gameObject);
        var replacement = GameObject.Instantiate(replaceWith, transform.position, Quaternion.identity, transform.parent);
        replacement.name = $"{replaceWith.name} <- {this.gameObject.name}";

    }
}
