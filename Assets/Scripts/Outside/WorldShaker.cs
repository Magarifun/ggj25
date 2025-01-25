using System.Collections.Generic;
using UnityEngine;

public class WorldShaker : MonoBehaviour
{
    public float shakeOffset = 1f;
    public float rollDegrees = 10;
    public GameObject[] unshakeables;

    public void Shake()
    {
        GameObject temporaryWorld = new ("Temporary World");
        temporaryWorld.transform.position = transform.position;
        List<Transform> children = new ();
        foreach (Transform child in transform)
        {
            if (child.position.y < transform.position.y)
            {
                children.Add(child);
            }
        }
        foreach (Transform child in children)
        {
            child.parent = temporaryWorld.transform;
        }
        foreach (GameObject unshakeable in unshakeables)
        {
            unshakeable.transform.parent = transform;
        }
        temporaryWorld.transform.Translate(Vector3.up * shakeOffset);
        foreach (Transform child in children)
        {
            child.parent = transform;
        }
        Destroy(temporaryWorld, 1f);
    }

    public void Roll(bool clockwise)
    {
        foreach (GameObject unshakeable in unshakeables)
        {
            unshakeable.transform.parent = null;
        }
        transform.Rotate(clockwise ? Vector3.back : Vector3.forward, rollDegrees);
        foreach (GameObject unshakeable in unshakeables)
        {
            unshakeable.transform.parent = this.transform;
        }
    }
}
