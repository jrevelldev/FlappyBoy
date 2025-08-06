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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb.simulated = false;
    }

    void Update()
    {
        if (!hasStarted)
        {
            IdleAnimation();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hasStarted = true;
                rb.simulated = true;
                Flap();
            }
        }
        else
        {
            FlapAnimation();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Flap();
            }
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
