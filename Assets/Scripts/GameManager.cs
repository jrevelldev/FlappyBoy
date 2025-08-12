using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        Idle,
        Playing,
        Dead
    }

    public GameState CurrentState { get; private set; } = GameState.Idle;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Ensure idle music plays at start (loop on)
        AudioManager.Instance.PlayMusic(AudioManager.Instance.musicIdle, loop: true);
    }

    public void StartGame()
    {
        if (CurrentState != GameState.Idle) return;

        CurrentState = GameState.Playing;
        Debug.Log("Game Started!");
        AudioManager.Instance.PlayMusic(AudioManager.Instance.musicPlaying, loop: true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxStart);
    }

    public void GameOver()
    {
        if (CurrentState != GameState.Playing) return;

        CurrentState = GameState.Dead;
        Debug.Log("Game Over!");
        AudioManager.Instance.PlayMusic(AudioManager.Instance.musicDead, loop: false);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxCrash);
    }

    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}
