using UnityEngine;

public class ClickFewTimes: MonoBehaviour
{
    public GameObject replacement;
    public int n = 3;

    public void OnClicked()
    {
        n--;
        if (n <= 0)
        {
            replacement.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
