using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public Sprite[] idleSprites;
    public Sprite[] flapSprites;
    public float idleAnimSpeed = 0.5f;
    public float flapAnimSpeed = 0.2f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool hasStarted = false;
    private int currentFrame = 0;
    private float animTimer = 0f;
    private bool isFlapping = false;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb.simulated = false;
    }

    void Update()
    {
        if (isDead) return;

        if (GameManager.Instance.CurrentState == GameManager.GameState.Idle)
        {
            IdleAnimation();
            if (JumpPressed())
            {
                GameManager.Instance.StartGame();
                rb.simulated = true;
                Flap();
            }
        }
        else if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
        {
            FlapAnimation();
            if (JumpPressed())
            {
                Flap();
            }
        }
        else
        {
            FlapAnimation();
            if (JumpPressed())
            {
                Flap();
            }
        }
    }

    bool JumpPressed()
    {
        // Works for both desktop and mobile
        return Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
        {
            isDead = true;

            // Freeze the player's physics so they stop moving independently
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;

            // Stick to the obstacle
            transform.SetParent(collision.transform);
            transform.localPosition = transform.localPosition; // preserve world position

            // Notify GameManager
            GameManager.Instance.GameOver();

            Debug.Log("You Died!");
        }
    }

    void Flap()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isFlapping = true;
        animTimer = 0f;
        currentFrame = 0;
    }

    void IdleAnimation()
    {
        animTimer += Time.deltaTime;
        if (animTimer >= idleAnimSpeed)
        {
            animTimer = 0f;
            currentFrame = (currentFrame + 1) % idleSprites.Length;
            spriteRenderer.sprite = idleSprites[currentFrame];
        }
    }

    void FlapAnimation()
    {
        if (!isFlapping) return;

        animTimer += Time.deltaTime;
        if (animTimer >= flapAnimSpeed)
        {
            animTimer = 0f;
            currentFrame = (currentFrame + 1) % flapSprites.Length;
            spriteRenderer.sprite = flapSprites[currentFrame];
        }
    }
}
