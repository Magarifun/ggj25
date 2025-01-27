using UnityEngine;
using TMPro;

public class ResetCounter : MonoBehaviour
{
    public TextMeshProUGUI textField; // Assign your TextMeshPro field here
    private const string ResetKey = "ResetCount"; // PlayerPrefs key for the reset count

    private void Start()
    {
        // Initialize the text with the current letter
        UpdateText();
    }

    public void OnResetButtonPressed()
    {
        // Increment the reset count
        int resetCount = PlayerPrefs.GetInt(ResetKey, 0);
        resetCount++;
        PlayerPrefs.SetInt(ResetKey, resetCount);
        PlayerPrefs.Save();

        // Update the text field
        UpdateText();
    }

    [ContextMenu("Reset PlayerPrefs")]
    public void ResetPlayerPrefs()
    {
        // Reset the PlayerPrefs for the ResetCount key
        PlayerPrefs.DeleteKey(ResetKey);
        PlayerPrefs.SetInt(ResetKey, 0);
        PlayerPrefs.Save();

        // Update the text field to reflect the reset
        UpdateText();
    }

    private void UpdateText()
    {
        // Get the current reset count
        int resetCount = PlayerPrefs.GetInt(ResetKey, 0);

        // Calculate the corresponding letter sequence starting from "B"
        string letter = ConvertToLetters(resetCount);

        // Update the TextMeshPro text field
        if (textField != null)
        {
            textField.text = letter;
        }
    }

    private string ConvertToLetters(int number)
    {
        // Adjust the starting index to begin from 'B'
        number += 2; // Shift the index to make "B" the starting letter (instead of "A")

        // Convert a 1-based index into a letter sequence
        number--; // Make it 0-based for calculation
        string result = string.Empty;

        do
        {
            result = (char)('A' + (number % 26)) + result;
            number = (number / 26) - 1;
        } while (number >= 0);

        return result;
    }
}
