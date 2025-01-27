using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResetCounter))]
public class ResetCounterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Call the base class to ensure the default Inspector is drawn
        base.OnInspectorGUI();

        // Add a button to the Inspector
        ResetCounter resetCounter = (ResetCounter)target;
        
        if (GUILayout.Button("Reset PlayerPrefs"))
        {
            resetCounter.ResetPlayerPrefs();
        }
    }
}