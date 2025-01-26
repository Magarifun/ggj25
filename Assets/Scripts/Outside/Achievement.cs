using UnityEngine;

public class Achievement : MonoBehaviour
{
    public string elementTag;
    public int elementCount;
    public bool unlocked;
    public GameObject unlockedTick;

    public void Unlock()
    {
        unlocked = true;
        unlockedTick.SetActive(true);
    }
}
