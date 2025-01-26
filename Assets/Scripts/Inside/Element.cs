using System;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    const float STANDUP_COEFFICIENT = 0.005f; // degrees per second per degree deviation

    public static readonly Dictionary<string, int> elementCount = new ();

    public string[] elementTags;
    public bool standingUp = false;
    public float standUpBoost = 1f;
    public GameObject[] upgrades;
    private int upgradeIndex = 0;
    private bool usedInRule = false;

    public bool UsedInRule => usedInRule;

    public string OwnElementTag => elementTags[0];

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
                if (upgrades[i])
                {
                    upgrades[i].SetActive(false);
                }
            }
            upgrades[upgradeIndex].SetActive(true);
        }
        foreach (string elementTag in elementTags)
        {
            elementCount[elementTag] = elementCount.ContainsKey(elementTag) ? elementCount[elementTag] + 1 : 1;
        }
    }

    private void OnDestroy()
    {
        foreach (string elementTag in elementTags)
        {
            elementCount[OwnElementTag]--;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        usedInRule = false;
        if (standingUp && TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2d))
        {
            float deviationDegrees = Vector3.SignedAngle(Vector3.up, transform.up, Vector3.forward);
            float torque = -STANDUP_COEFFICIENT * standUpBoost * deviationDegrees;
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

    public void MarkAsUsedInRule()
    {
        usedInRule = true;
    }
}
