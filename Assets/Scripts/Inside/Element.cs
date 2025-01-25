using System;
using UnityEngine;

public class Element : MonoBehaviour
{
    const float STANDUP_COEFFICIENT = 0.005f; // degrees per second per degree deviation

    public string[] elementTags;
    public bool standingUp = false;
    public GameObject[] upgrades;
    private int upgradeIndex = 0;

    internal void Remove()
    {
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tag = "Element";
        if (upgrades.Length > 0)
        {
            for (int i = 1; i < upgrades.Length; i++)
            {
                upgrades[i].SetActive(false);
            }
            upgrades[upgradeIndex].SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (standingUp && TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2d))
        {
            float deviationDegrees = Vector3.SignedAngle(Vector3.up, transform.up, Vector3.forward);
            float torque = -STANDUP_COEFFICIENT * deviationDegrees;
            // Apply torch
            rb2d.AddTorque(torque);
        }

    }

    public void Upgrade()
    {
        if (upgradeIndex < upgrades.Length - 1)
        {
            upgrades[upgradeIndex].SetActive(false);
            upgradeIndex++;
            GameObject nextUpgrade = upgrades[upgradeIndex];
            if (nextUpgrade)
            {
                upgrades[upgradeIndex].SetActive(true);
            }
            else
            {
                Destroy(gameObject);
            }           
        }
    }
}
