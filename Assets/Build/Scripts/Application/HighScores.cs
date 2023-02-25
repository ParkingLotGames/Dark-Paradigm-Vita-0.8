using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages uploading and downloading of high scores from an online database using the Dreamlo API.
/// </summary>
public class HighScores : MonoBehaviour
{
    #region Variables

    static HighScores instance;
    const string privateKey = "-izzGk5gCE2bdtIRfxBC9wJm3kt99T3EyJpVl30kVh3w";
    const string publicKey = "609560858f40bb0ca0aa446c";
    const string webURL = "http://dreamlo.com/lb/";

    public PlayerScore[] scoreList;
    DisplayHighscores highscores;

    #endregion

    #region Methods

    /// <summary>
    /// Sets the static instance of the HighScores class.
    /// </summary>
    void Awake()
    {
        instance = this;
        highscores = GetComponent<DisplayHighscores>();
    }

    /// <summary>
    /// Uploads the player's score to the online database.
    /// </summary>
    /// <param name="username">The player's username.</param>
    /// <param name="score">The player's score.</param>
    public static void UploadScore(string username, int score)
    {
        instance.StartCoroutine(instance.DatabaseUpload(username, score));
    }

    /// <summary>
    /// Coroutine to upload the player's score to the online database.
    /// </summary>
    /// <param name="username">The player's username.</param>
    /// <param name="score">The player's score.</param>
    IEnumerator DatabaseUpload(string userame, int score)
    {
        WWW www = new WWW(webURL + privateKey + "/add/" + WWW.EscapeURL(userame) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            DownloadScores();
        }
        else
        {
            print("Error uploading" + www.error);
        }
    }

    /// <summary>
    /// Initiates the download of high scores from the online database.
    /// </summary>
    public void DownloadScores()
    {
        StartCoroutine("DatabaseDownload");
    }

    /// <summary>
    /// Coroutine to download high scores from the online database.
    /// </summary>
    IEnumerator DatabaseDownload()
    {
        //WWW www = new WWW(webURL + publicKey + "/pipe/"); //Gets the whole list
        WWW www = new WWW(webURL + publicKey + "/pipe/0/10"); //Gets top 10
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            OrganizeInfo(www.text);
            highscores.SetScoresToMenu(scoreList);
        }
        else
        {
            print("Error uploading" + www.error);
        }
    }

    /// <summary>
    /// Organizes the raw data retrieved from the online database into an array of PlayerScore objects.
    /// Effectively rows in the screen.
    /// </summary>
    /// <param name="rawData">The raw data retrieved from the online database.</param>
    void OrganizeInfo(string rawData)
    {
        string[] entries = rawData.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        scoreList = new PlayerScore[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            scoreList[i] = new PlayerScore(username, score);
            print(scoreList[i].username + ": " + scoreList[i].score);
        }
    }

    #endregion
}

public struct PlayerScore
{
    public string username;
    public int score;

    public PlayerScore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}