using UnityEngine;

[CreateAssetMenu(fileName = "GameUITextData", menuName = "UI/Game UIText Data")]
public class GameUITextData : ScriptableObject
{
    [Header("UI Messages")]
    public string startMessage = "Press SPACE to Start";
    public string deadMessage = "You Died!\nPress R to Restart";
}
