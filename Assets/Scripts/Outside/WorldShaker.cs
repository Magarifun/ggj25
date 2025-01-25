using UnityEngine;
using UnityEngine.UIElements;

public class WorldShaker : MonoBehaviour
{
    public float shakeOffset = 1f;
    public GameObject[] unshakeables;

    public void Shake()
    {
        foreach (GameObject unshakeable in unshakeables)
        {
            unshakeable.transform.parent = null;
        }
        transform.Translate(Vector3.up * shakeOffset);
        foreach (GameObject unshakeable in unshakeables)
        {
            unshakeable.transform.parent = this.transform;
        }
    }
}
