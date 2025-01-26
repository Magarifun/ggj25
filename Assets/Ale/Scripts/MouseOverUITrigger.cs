using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator enterAnimator; // Reference to the Animator for the enter animation
    public string enterAnimationTrigger = "EnterAnimation"; // Trigger for the enter animation

    public Animator exitAnimator; // Reference to the Animator for the exit animation
    public string exitAnimationTrigger = "ExitAnimation"; // Trigger for the exit animation

    public Button targetButton; // Reference to the button to enable (optional)

    private void Start()
    {
        // Enable the button immediately, if assigned
        if (targetButton != null)
        {
            targetButton.interactable = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Trigger the enter animation
        if (enterAnimator != null)
        {
            enterAnimator.SetTrigger(enterAnimationTrigger);
        }
        else
        {
            Debug.LogWarning("Enter Animator is not assigned.");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Trigger the exit animation
        if (exitAnimator != null)
        {
            exitAnimator.SetTrigger(exitAnimationTrigger);
        }
        else
        {
            Debug.LogWarning("Exit Animator is not assigned.");
        }
    }
}
