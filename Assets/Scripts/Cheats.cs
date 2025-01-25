using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cheats : MonoBehaviour
{
    public KeyCode[] cheatCodes;
    public UnityEvent[] events;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cheatCodes.Length; i++)
        {
            if (Input.GetKeyDown(cheatCodes[i]))
            {
                events[i].Invoke();
            }
        }
    }
}
