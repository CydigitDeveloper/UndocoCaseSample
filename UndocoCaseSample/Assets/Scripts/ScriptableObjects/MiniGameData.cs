using UnityEngine;

[CreateAssetMenu(fileName = "NewMiniGame", menuName = "MiniGames/Create New MiniGame")]
public class MiniGameData : ScriptableObject
{
    public string gameName;
    public string sceneName;
    public Sprite gameIcon;
    [TextArea] public string description;
}