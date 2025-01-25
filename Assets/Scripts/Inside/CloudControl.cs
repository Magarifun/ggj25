using Unity.VisualScripting;
using UnityEngine;

public class CloudControl : MonoBehaviour
{
    [SerializeField] Transform[] cloudAltitudes;
    
    public Transform GetCloudAltitudeFor(Element element)
    {
        return cloudAltitudes[Random.Range(0, cloudAltitudes.Length)];
    }
}
