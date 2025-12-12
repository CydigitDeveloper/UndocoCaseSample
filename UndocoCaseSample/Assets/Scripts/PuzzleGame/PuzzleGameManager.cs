using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGameManager : MonoBehaviour
{
    public int totalPieces = 6;
    
    [Header("UI References")]
    public GameObject winPanel;
    public TextMeshProUGUI pieceCountText;
    public Button restartButton;
    public Button returnToMainMenuButton;
    
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip winSound;

    private int _placedPieces = 0;

    void Start()
    {
        UpdatePieceUI();
        
        if (winPanel != null) winPanel.SetActive(false);
        
        restartButton.onClick.AddListener(() => {
            MiniGameManager.Instance.RestartCurrentGame();
        });
        
        returnToMainMenuButton.onClick.AddListener(() => {
            MiniGameManager.Instance.ReturnToMainMenu();
        });
    }

    public void PiecePlaced()
    {
        _placedPieces++;
        UpdatePieceUI();
        
        if (_placedPieces >= totalPieces)
        {
            GameFinished();
        }
        else
        {
            if (audioSource && correctSound) audioSource.PlayOneShot(correctSound);
        }
    }
    
    private void UpdatePieceUI()
    {
        if(pieceCountText != null)
            pieceCountText.text = $"Parçalar: {_placedPieces}/{totalPieces}";
    }

    private void GameFinished()
    {
        Debug.Log("Puzzle Tamamlandı!");
        if (winPanel != null) winPanel.SetActive(true);
        if (audioSource && winSound) audioSource.PlayOneShot(winSound);
    }
}