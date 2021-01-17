using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int m_CurrentLevel;
    [SerializeField] private const int LastLevel = 2;

    private void Awake() 
    {
        instance = this;
        m_CurrentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void EndGame()
    {
        UIManager.Instance.GameOver();
        Debug.Log("GameOver");
        Time.timeScale = 0;
    }
    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(m_CurrentLevel);
    }
    public void NextLevel()
    {
        m_CurrentLevel++;
        if(m_CurrentLevel <= LastLevel)
        {
            SceneManager.LoadScene(m_CurrentLevel);
        }
    }
    public void OpenMenu()
    {

    }
}
