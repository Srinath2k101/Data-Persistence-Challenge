using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public TMP_InputField playerNameField;
    public TextMeshProUGUI bestScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameDataManager.Instance.LoadGameData();
        if(!string.IsNullOrWhiteSpace(GameDataManager.Instance.HighScoreHolder))
        {
            bestScoreText.text = $"Best Score \n {GameDataManager.Instance.HighScoreHolder} : {GameDataManager.Instance.HighScore}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    public void PlayGame()
    {
        if(string.IsNullOrWhiteSpace(playerNameField.text))
        {
            GameDataManager.Instance.PlayerName = "Player";
        }
        else
        {
            GameDataManager.Instance.PlayerName = playerNameField.text;
        }
        Debug.Log(GameDataManager.Instance.PlayerName);
        if (GameDataManager.Instance.PlayerName != null)
        {
            SceneManager.LoadScene(1);
        }

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        GameDataManager.Instance.SaveGameData();
    }
}
