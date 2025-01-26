using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Achievements : MonoBehaviour
{
    private string[] elementTags;
    private int[] elementCounts;
    private bool[] achievementsUnlocked;
    private Achievement[] achievements;
    private bool isSummary = false;

    public GameObject panelForSummary;
    public GameObject panelForNotifications;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panelForSummary.SetActive(true);
        achievements = GetComponentsInChildren<Achievement>();
        elementTags = new string[achievements.Length];
        elementCounts = new int[achievements.Length];
        achievementsUnlocked = new bool[achievements.Length];
        for (int i = 0; i < achievements.Length; i++)
        {
            RefreshAchievementData(i);
        }
        panelForSummary.SetActive(false);
        panelForNotifications.SetActive(true);
        isSummary = false;
    }

    private void RefreshAchievementData(int i)
    {
        elementTags[i] = achievements[i].elementTag;
        elementCounts[i] = achievements[i].elementCount;
        achievementsUnlocked[i] = achievements[i].unlocked;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < elementTags.Length; i++)
        {
            if (achievementsUnlocked[i])
            {
                continue;
            }

            if (Element.elementCount.ContainsKey(elementTags[i]) && Element.elementCount[elementTags[i]] >= elementCounts[i])
            {
                UnlockAchievement(i);
            }
        }
    }

    private void UnlockAchievement(int i)
    {
        Debug.Log($"Achievement unlocked: {elementTags[i]} x{elementCounts[i]}");
        achievements[i].Unlock();
        RefreshAchievementData(i);
        GameObject notification = Instantiate(achievements[i].gameObject, panelForNotifications.transform);
        Invoke(nameof(RemoveOldestNotification), 5);
    }

    public void TogglePanels()
    {
        if (isSummary)
        {
            panelForSummary.SetActive(false);
            panelForNotifications.SetActive(true);
        }
        else
        {
            panelForSummary.SetActive(true);
            panelForNotifications.SetActive(false);
        }
        isSummary = !isSummary;
    }

    public void RemoveOldestNotification()
    {
        if (panelForNotifications.transform.childCount > 0)
        {
            Destroy(panelForNotifications.transform.GetChild(0).gameObject);
        }
    }
}
