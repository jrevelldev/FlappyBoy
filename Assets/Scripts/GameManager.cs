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

    public void StartGame()
    {
        if (CurrentState != GameState.Idle) return;

        CurrentState = GameState.Playing;
        Debug.Log("Game Started!");
    }

    public void GameOver()
    {
        if (CurrentState != GameState.Playing) return;

        CurrentState = GameState.Dead;
        Debug.Log("Game Over!");
    }

    public void ResetGame()
    {
        // Optional: reload scene, fade, etc.
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}
