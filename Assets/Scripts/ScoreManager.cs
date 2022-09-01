using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    [System.Serializable]
    class ScoreData
    {
        public int score;
        public string name;
    }

    public static ScoreManager instance;
    public int highScoreScore;
    public string highScoreName;

    public string currName;


    // Start is called before the first frame update
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);
        LoadScoreFromFile();            
    }

    // Update is called once per frame
    
    public void SaveScore(int points)
    {
        highScoreScore = points;
        highScoreName = currName;
        SaveScoreToFile();
    }

    public void SaveScoreToFile()
    {
        ScoreData scoreData = new ScoreData();
        scoreData.score = highScoreScore;
        scoreData.name = highScoreName;

        string s = JsonUtility.ToJson(scoreData);

        File.WriteAllText(Application.persistentDataPath + "/scorefile.json", s);
    }

    public void LoadScoreFromFile()
    {
        string path = Application.persistentDataPath + "/scorefile.json";

        if (File.Exists(path))
        {
            string s = File.ReadAllText(path);

            ScoreData scoreData = JsonUtility.FromJson<ScoreData>(s);
            highScoreName = scoreData.name;
            highScoreScore = scoreData.score;
        }
    }

    public string GetBestScoreMessage()
    {
        string s;
        if (highScoreScore > 0)
        {
            s = "BEST SCORE: " + highScoreName + " " + highScoreScore+" points";
        }
        else
        {
            s = "Nobody played the game yet";
        }

        return(s);
    }
}
