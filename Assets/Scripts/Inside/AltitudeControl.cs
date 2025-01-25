using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Element))]
public class AltitudeControl : MonoBehaviour
{
    public float pullDownFactor = 0.5f;
    public float maxRandomOffset = 1f;

    private Transform altitudeAnchor;
    private Rigidbody2D rb2d;
    private float offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        CloudControl cloudControl = GameObject.FindAnyObjectByType<CloudControl>();
        altitudeAnchor = cloudControl.GetAltitudeFor(GetComponent<Element>());
        offset = Random.Range(-maxRandomOffset, maxRandomOffset);
    }

    void FixedUpdate()
    {
        float delta = transform.position.y - (altitudeAnchor.position.y + offset);
        if (delta > 0)
        {
            rb2d.AddForce(Vector2.down * delta, ForceMode2D.Force);
        }
        offset += Random.Range(-maxRandomOffset / 100, maxRandomOffset / 100) * Time.fixedDeltaTime;
    }
}
