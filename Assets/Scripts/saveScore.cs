using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class SaveScore : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField scoreInputField;

    private string playerName;
    private int score;

    public void SubmitScoreFromButton()
    {
        // 1. Read Name Input
        if (nameInputField != null)
        {
            playerName = nameInputField.text.Trim();
        }
        else if (GetComponent<TMP_InputField>() != null)
        {
            playerName = GetComponent<TMP_InputField>().text.Trim();
        }

        // 2. Read Score Input
        if (scoreInputField != null)
        {
            int.TryParse(scoreInputField.text.Trim(), out score);
        }

        // 3. Validate and Submit
        if (!string.IsNullOrEmpty(playerName))
        {
            Debug.Log($"Saving - Name: {playerName} | Score: {score}");
            StartCoroutine(connectToPHP());
        }
        else
        {
            Debug.LogWarning("Player name field is empty!");
        }
    }

    IEnumerator connectToPHP()
    {
        string url = "http://localhost/updateScore_b.php";
        url += "?name=" + UnityWebRequest.EscapeURL(playerName) + "&score=" + score;

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("DB updated. Server response:\n" + www.downloadHandler.text);
                SceneManager.LoadScene("exit");
            }
            else
            {
                Debug.LogError("Error updating database: " + www.error);
            }
        }
    }
}