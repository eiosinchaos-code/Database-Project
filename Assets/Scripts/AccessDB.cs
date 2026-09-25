using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class AccessDB : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;

    // Pass a default name parameter so updateScore.php doesn't throw an error
    private string url = "http://localhost/updateScore_b.php?name=newUser&score=0";

    IEnumerator Start()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string result = www.downloadHandler != null ? www.downloadHandler.text : "";
                Debug.Log("Data received: " + result);

                if (highScoreText != null)
                {
                    highScoreText.text = result;
                }
            }
            else
            {
                Debug.LogError("Error fetching score: " + www.error);
            }
        }
    }
}