using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button openPauseButton;

    private bool _isPaused = false;

    private void Start()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        
        if (resumeButton) resumeButton.onClick.AddListener(ResumeGame);
        
        if (restartButton) restartButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            if (MiniGameManager.Instance != null) MiniGameManager.Instance.RestartCurrentGame();
        });

        if (homeButton) homeButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            if (MiniGameManager.Instance != null) MiniGameManager.Instance.ReturnToMainMenu();
        });
        
        if (openPauseButton) openPauseButton.onClick.AddListener(PauseGame);
    }

    public void PauseGame()
    {
        _isPaused = true;
        if (pausePanel) pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        _isPaused = false;
        if (pausePanel) pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}