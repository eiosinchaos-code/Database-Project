using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class FetchTwoColumnScores : MonoBehaviour
{
    [SerializeField] private TMP_Text namesColumnText;
    [SerializeField] private TMP_Text scoresColumnText;

    private string url = "http://localhost/updateScore_b.php?name=newUser&score=0";

    IEnumerator Start()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string rawData = www.downloadHandler != null ? www.downloadHandler.text : "";
                ParseAndDisplay(rawData);
            }
            else
            {
                Debug.LogError("Error fetching scores: " + www.error);
            }
        }
    }

    void ParseAndDisplay(string rawData)
    {
        namesColumnText.text = "<b>NAME</b>\n\n";
        scoresColumnText.text = "<b>SCORE</b>\n\n";

        string[] lines = rawData.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            if (line.Contains("-"))
            {
                string[] parts = line.Split('-');
                namesColumnText.text += parts[0].Trim() + "\n";
                scoresColumnText.text += parts[1].Trim() + "\n";
            }
        }
    }
}