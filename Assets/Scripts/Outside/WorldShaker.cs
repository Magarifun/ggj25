using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WorldShaker : MonoBehaviour
{
    [Header("UI Setup")]
    public GameObject controls;
    public float delayBeforeControlsEnabled;

    [Header("Effects setup")]
    public float shakeOffset = 1f;
    public float rollDegrees = 10;
    public GameObject[] unshakeables;

    public void Start()
    {
        controls.SetActive(false);
        Invoke("EnableControls", delayBeforeControlsEnabled);
    }

    public void EnableControls()
    {
        controls.SetActive(true);
    }

    public void Shake()
    {
        GameObject temporaryWorld = new("Temporary World");
        temporaryWorld.transform.position = transform.position;
        List<Transform> children = new();
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
        SoundManager.Instance.ShakeSfx();
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
        SoundManager.Instance.RollSfx();
    }

    public void Quake(string quakeableTag, int factor)
    {
        int count = 0;
        foreach (Element element in GetComponentsInChildren<Element>())
        {
            if (element.elementTags.Contains(quakeableTag))
            {
                element.gameObject.transform.localScale *= factor;
                count++;
                if (count >= 20)
                {
                    return;
                }
            }

        }
    }

    public void Tide()
    {
        foreach (Tide element in GetComponentsInChildren<Tide>())
        {
            element.StartTide();
        }
    }
}
