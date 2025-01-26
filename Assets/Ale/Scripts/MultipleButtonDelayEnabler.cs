using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MultipleButtonDelayEnabler : MonoBehaviour
{
    [System.Serializable]
    public class ButtonDelay
    {
        public Button button;
        public float delay;
    }

    public ButtonDelay[] buttonDelays; // Array of buttons and delays

    private void Start()
    {
        foreach (var buttonDelay in buttonDelays)
        {
            if (buttonDelay.button != null)
            {
                StartCoroutine(EnableButtonAfterDelay(buttonDelay));
            }
        }
    }

    private IEnumerator EnableButtonAfterDelay(ButtonDelay buttonDelay)
    {
        // Disable the button initially
        buttonDelay.button.interactable = false;

        // Wait for the specified delay
        yield return new WaitForSeconds(buttonDelay.delay);

        // Enable the button
        buttonDelay.button.interactable = true;
    }
}
