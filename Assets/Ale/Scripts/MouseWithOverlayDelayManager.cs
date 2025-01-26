using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MouseOverWithDelayManager : MonoBehaviour
{
    [System.Serializable]
    public class ButtonConfig
    {
        public Button button;
        public Animator enterAnimator;
        public string enterAnimationTrigger = "EnterAnimation";
        public Animator exitAnimator;
        public string exitAnimationTrigger = "ExitAnimation";
        public float delay = 2.0f;
    }

    public List<ButtonConfig> buttonConfigs;

    private Dictionary<Button, bool> interactionEnabled = new Dictionary<Button, bool>();

    private void Start()
    {
        foreach (var config in buttonConfigs)
        {
            interactionEnabled[config.button] = false;
            StartCoroutine(EnableInteractionAfterDelay(config));
        }
    }

    private IEnumerator EnableInteractionAfterDelay(ButtonConfig config)
    {
        yield return new WaitForSeconds(config.delay);
        interactionEnabled[config.button] = true;
        config.button.interactable = true; // Enable the button functionality
    }

    public void OnPointerEnter(Button button)
    {
        if (interactionEnabled.ContainsKey(button) && interactionEnabled[button])
        {
            var config = buttonConfigs.Find(c => c.button == button);
            if (config != null && config.enterAnimator != null)
            {
                config.enterAnimator.SetTrigger(config.enterAnimationTrigger);
            }
        }
    }

    public void OnPointerExit(Button button)
    {
        if (interactionEnabled.ContainsKey(button) && interactionEnabled[button])
        {
            var config = buttonConfigs.Find(c => c.button == button);
            if (config != null && config.exitAnimator != null)
            {
                config.exitAnimator.SetTrigger(config.exitAnimationTrigger);
            }
        }
    }
}
