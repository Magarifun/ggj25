using UnityEngine;

public class Tide : MonoBehaviour
{

    public void StartTide()
    {
        var animator = GetComponent<Animator>();
        animator.SetTrigger("Tide");
    }
}
