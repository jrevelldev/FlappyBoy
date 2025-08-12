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

    private float deathTime = -1f;
    private float restartDelay = 2f; // seconds to wait before allowing restart

    bool RestartPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space)) return true;      // desktop
        if (Input.GetMouseButtonDown(0)) return true;          // desktop web tap/click
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) return true; // mobile
        return false;
    }

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
                deathTime = -1f;  // reset the death timer if returning to idle (safety)
                break;

            case GameManager.GameState.Playing:
                messageText.text = "";
                break;

            case GameManager.GameState.Dead:
                messageText.text = uiTextData.deadMessage;
                // Record the time of death
                if (deathTime < 0f)
                    deathTime = Time.time;
                break;
        }

        // Handle restart after delay
        if (GameManager.Instance.CurrentState == GameManager.GameState.Dead)
        {
            if (RestartPressed() && Time.time >= deathTime + restartDelay)
            {
                ResetScore();
                GameManager.Instance.ResetGame();
                deathTime = -1f; // ready for next cycle
            }
        }

    }
}
