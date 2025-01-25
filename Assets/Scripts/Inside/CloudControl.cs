using Unity.VisualScripting;
using UnityEngine;

public class CloudControl : MonoBehaviour
{
    [SerializeField] Transform[] cloudAltitudes;
    [SerializeField] Transform sunAltitude;
    
    public Transform GetAltitudeFor(Element element)
    {
        foreach (string elementTag in element.elementTags)
        {
            if (elementTag == "sun")
            {
                return sunAltitude;
            }
        }
        return cloudAltitudes[Random.Range(0, cloudAltitudes.Length)];
    }
}
