using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;

    private void Start()
    {
        if (MiniGameManager.Instance == null || MiniGameManager.Instance.availableGames.Count == 0)
        {
            return;
        }
        
        foreach (var gameData in MiniGameManager.Instance.availableGames)
        {
            CreateGameButton(gameData);
        }
    }

    private void CreateGameButton(MiniGameData data)
    {
        GameObject btnObj = Instantiate(buttonPrefab, container);
        
        TextMeshProUGUI btnText = btnObj.GetComponentInChildren<TextMeshProUGUI>();
        if(btnText) btnText.text = data.gameName;
        
        Image btnImage = btnObj.GetComponent<Image>();
        if(btnImage) btnImage.sprite = data.gameIcon;
        
        Button btn = btnObj.GetComponent<Button>();
        
        btn.onClick.AddListener(() => {
            MiniGameManager.Instance.LoadMiniGame(data);
        });
    }
}
