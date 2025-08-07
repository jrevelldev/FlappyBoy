using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameUITextData uiTextData; // ← Add this

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        switch (GameManager.Instance.CurrentState)
        {
            case GameManager.GameState.Idle:
                messageText.text = uiTextData.startMessage;
                break;
            case GameManager.GameState.Playing:
                messageText.text = "";
                break;
            case GameManager.GameState.Dead:
                messageText.text = uiTextData.deadMessage;
                break;
        }

        if (GameManager.Instance.CurrentState == GameManager.GameState.Dead && Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.ResetGame();
        }
    }
}
