using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public TMP_InputField playerNameField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    public void PlayGame()
    {
        GameDataManager.Instance.PlayerName = playerNameField.text;
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
    }
}
