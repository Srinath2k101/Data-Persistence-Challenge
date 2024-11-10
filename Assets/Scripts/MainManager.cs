using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text highScoreText;
    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;



    
    // Start is called before the first frame update
    void Start()
    {
        if(GameDataManager.Instance != null)
        {
            if (GameDataManager.Instance.HighScore != -1 && GameDataManager.Instance.isFirstGamePlayed)
            {
                UpdateHighScoreBoard();
            }
        }
        
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if(!GameDataManager.Instance.isFirstGamePlayed)
        {
            GameDataManager.Instance.isFirstGamePlayed = true;
        }
        UpdateHighScore();
        UpdateHighScoreBoard();
        GameDataManager.Instance.SaveGameData();
        
    }

    void UpdateHighScoreBoard()
    {
        highScoreText.text = $"Best Score : {GameDataManager.Instance.HighScoreHolder} : {GameDataManager.Instance.HighScore}";
    }

    void UpdateHighScore()
    {
        if(GameDataManager.Instance.HighScore < m_Points)
        {
            GameDataManager.Instance.HighScore = m_Points;
            GameDataManager.Instance.HighScoreHolder = GameDataManager.Instance.PlayerName;
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

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }

}
