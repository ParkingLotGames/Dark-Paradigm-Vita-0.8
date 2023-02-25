using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script to display highscores in a menu. 
/// </summary>
public class DisplayHighscores : MonoBehaviour
{
    /// <summary>
    /// An array of Text components that display the names of the players who achieved highscores.
    /// </summary>
    [SerializeField] Text[] names;

    /// <summary>
    /// An array of Text components that display the scores of the players who achieved highscores.
    /// </summary>
    [SerializeField] Text[] scores;

    /// <summary>
    /// The number of Text components that display the names of the players who achieved highscores.
    /// </summary>
    int namesLength;

    /// <summary>
    /// The number of Text components that display the scores of the players who achieved highscores.
    /// </summary>
    int scoresLength;

    /// <summary>
    /// The HighScores component that is used to download the scores.
    /// </summary>
    HighScores highscores;

    /// <summary>
    /// This function is called when the object is enabled.
    /// </summary>
    void OnEnable() //Fetches the Data at the beginning
    {
        // Set the number of names and scores to the length of the arrays.
        namesLength = names.Length;
        scoresLength = scores.Length;

        // Set the text of each name to "Fetching...".
        for (int i = 0; i < namesLength; i++)
        {
            names[i].text = i + 1 + ". Fetching...";
        }

        // Get the HighScores component and start refreshing the highscores.
        highscores = GetComponent<HighScores>();
        StartCoroutine("RefreshHighscores");
    }

    /// <summary>
    /// This function is called when the object is disabled.
    /// </summary>
    private void OnDisable()
    {
        // Stop refreshing the highscores.
        StopAllCoroutines();
    }

    /// <summary>
    /// Assigns proper name and score for each text value.
    /// </summary>
    /// <param name="highscoreList">The list of highscores to display.</param>
    public void SetScoresToMenu(PlayerScore[] highscoreList)
    {
        // Assign the proper name and score for each text value.
        for (int i = 0; i < namesLength; i++)
        {
            names[i].text = i + 1 + ". ";
            if (highscoreList.Length > i)
            {
                scores[i].text = highscoreList[i].score.ToString();
                names[i].text = highscoreList[i].username;
            }
        }
    }

    /// <summary>
    /// Refreshes the scores every 30 seconds.
    /// </summary>
    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscores.DownloadScores();
            // Wait for 30 seconds after refreshing.
            yield return new WaitForSeconds(30);
        }
    }
}
