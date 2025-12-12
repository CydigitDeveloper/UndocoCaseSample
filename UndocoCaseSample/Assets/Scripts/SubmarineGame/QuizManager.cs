using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public struct QuizQuestion
    {
        [TextArea] public string questionText;
        public string[] options;
        public int correctOptionIndex;
    }

    [Header("Game Settings")]
    public int totalChestsToWin = 5;
    public List<QuizQuestion> questions;

    [Header("References")]
    public SubmarineController playerSubmarine;
    public GameObject gamePanel;
    public TextMeshProUGUI chestCountText;

    [Header("Quiz UI References")]
    public GameObject quizPanel;
    public TextMeshProUGUI questionTextUI;
    public TextMeshProUGUI[] optionTextsUI;
    public GameObject resultPanel;
    public TextMeshProUGUI resultMessageText;
    public Button restartButton;
    public Button returnToMainMenuButton;
    
    private int _collectedChests = 0;
    private int _correctAnswersCount = 0;
    
    private int CurrentQuestionIndex => _collectedChests - 1;

    void Start()
    {
        UpdateChestUI();
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
        gamePanel.SetActive(true);
        
        restartButton.onClick.AddListener(() => {
            MiniGameManager.Instance.RestartCurrentGame();
        });
        
        returnToMainMenuButton.onClick.AddListener(() => {
            MiniGameManager.Instance.ReturnToMainMenu();
        });
    }

    public void OnChestCollected()
    {
        _collectedChests++;
        UpdateChestUI();

        AskQuestion(CurrentQuestionIndex);
    }

    private void UpdateChestUI()
    {
        if(chestCountText != null)
            chestCountText.text = $"Sandıklar: {_collectedChests}/{totalChestsToWin}";
    }

    private void AskQuestion(int index)
    {
        playerSubmarine.SetActiveState(false); 
        
        gamePanel.SetActive(false);
        quizPanel.SetActive(true);
        
        LoadQuestion(index);
    }

    private void LoadQuestion(int index)
    {
        if (index >= questions.Count)
        {
            Debug.LogWarning("Soru listesi bitti ama oyun devam ediyor!");
            return;
        }

        QuizQuestion currentQ = questions[index];
        
        questionTextUI.text = currentQ.questionText;

        for (int i = 0; i < optionTextsUI.Length; i++)
        {
            if (i < currentQ.options.Length)
                optionTextsUI[i].text = currentQ.options[i];
            else
                optionTextsUI[i].text = "";
        }
    }
    
    public void OnAnswerSelected(int selectedOptionIndex)
    {
        if (selectedOptionIndex == questions[CurrentQuestionIndex].correctOptionIndex)
        {
            _correctAnswersCount++;
            Debug.Log("Doğru Cevap! İnci Kazanıldı.");
        }
        else
        {
            Debug.Log("Yanlış Cevap.");
        }
        
        if (_collectedChests >= totalChestsToWin)
        {
            EndGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void ResumeGame()
    {
        quizPanel.SetActive(false);
        gamePanel.SetActive(true);
        
        playerSubmarine.SetActiveState(true);
    }

    private void EndGame()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(true);

        string message = "";
        
        if (_correctAnswersCount >= 4) message = "Kusursuz!";
        else if (_correctAnswersCount >= 2) message = "Aferin!";
        else message = "İyi!";

        resultMessageText.text = $"{message}\nToplanan İnci: {_correctAnswersCount}/5";
    }
}
