using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;
    
    public List<MiniGameData> availableGames;
    
    private MiniGameData _currentGame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadMiniGame(MiniGameData data)
    {
        _currentGame = data;
        SceneManager.LoadScene(data.sceneName);
    }

    public void RestartCurrentGame()
    {
        if (_currentGame != null)
        {
            SceneManager.LoadScene(_currentGame.sceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    public void ReturnToMainMenu()
    {
        _currentGame = null;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}