using System;
using UnityEngine;

public class Element : MonoBehaviour
{
    const float STANDUP_COEFFICIENT = 0.005f; // degrees per second per degree deviation

    public string[] elementTags;
    public bool standingUp = false;

    internal void Remove()
    {
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tag = "Element";
    }

    // Update is called once per frame
    void Update()
    {
        if (standingUp && TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2d))
        {
            float deviationDegrees = Vector3.SignedAngle(Vector3.up, transform.up, Vector3.forward);
            float torque = -STANDUP_COEFFICIENT * deviationDegrees;
            // Apply torch
            rb2d.AddTorque(torque);
        }

    }
}
