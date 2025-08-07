using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameUITextData uiTextData;

    [SerializeField] private TextMeshProUGUI scoreValueText;
    [SerializeField] private TextMeshProUGUI highScoreValueText;

    private int score = 0;
    private int highScore = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        scoreValueText.text = score.ToString();
        highScoreValueText.text = highScore.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
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

        if (GameManager.Instance.CurrentState == GameManager.GameState.Playing && Input.GetKeyDown(KeyCode.S))
        {
            AddScore(1);
        }

        if (GameManager.Instance.CurrentState == GameManager.GameState.Dead && Input.GetKeyDown(KeyCode.R))
        {
            ResetScore();
            GameManager.Instance.ResetGame();
        }

    }
}
