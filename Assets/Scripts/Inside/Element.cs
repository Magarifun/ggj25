using System;
using UnityEngine;

public class Element : MonoBehaviour
{
    public string[] elementTags;

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
        
    }
}
