using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    public string PlayerName;
    public string HighScoreHolder;
    public int HighScore = -1;
    public bool isFirstGamePlayed = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string HighScoreHolder;
        public int HighScore;
        public bool isFirstGamePlayed;
    }

    public void SaveGameData()
    {
        SaveData gameData = new SaveData();
        gameData.HighScoreHolder = HighScoreHolder;
        gameData.HighScore = HighScore;
        gameData.isFirstGamePlayed = isFirstGamePlayed;

        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.persistentDataPath + "/gamedata.json" , json);
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/gamedata.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData gameData = JsonUtility.FromJson<SaveData>(json);

            HighScoreHolder = gameData.HighScoreHolder;
            HighScore = gameData.HighScore;
            isFirstGamePlayed= gameData.isFirstGamePlayed;
        }
        else
        {
            return;
        }
    }


}
    