using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 movement = Vector2.zero;

    [SerializeField] private Sprite idle1, idle2, up1, up2, right1, right2, down1, down2, left1, left2;
    private int spriteIndex = 1;
    private float animationSpeedInSeconds = .25f;
    private float spriteTimer = 0;

    private float xDir = 0, yDir = 0;
    [SerializeField] private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleInput();
        UpdateSprite();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        movement = new Vector2(xDir, yDir).normalized;
        rb.velocity = movement * speed;
    }

    private void HandleInput()
    {
        xDir = Input.GetAxisRaw("Horizontal");
        yDir = Input.GetAxisRaw("Vertical");
    }

    private void UpdateSprite()
    {
        if (spriteTimer >= animationSpeedInSeconds)
        {
            spriteIndex = spriteIndex == 1 ? 2 : 1;
            spriteTimer = 0;
        }
        else
            spriteTimer += Time.deltaTime;

        sr.sprite = spriteIndex == 1 ? idle1 : idle2;
        if (xDir > 0) sr.sprite = spriteIndex == 1 ? right1 : right2;
        if (xDir < 0) sr.sprite = spriteIndex == 1 ? left1 : left2;
        if (yDir > 0) sr.sprite = spriteIndex == 1 ? up1 : up2;
        if (yDir < 0) sr.sprite = spriteIndex == 1 ? down1 : down2;
    }
}
