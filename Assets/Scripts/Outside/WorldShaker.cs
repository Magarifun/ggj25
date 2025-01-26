using System.Collections.Generic;
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
}
