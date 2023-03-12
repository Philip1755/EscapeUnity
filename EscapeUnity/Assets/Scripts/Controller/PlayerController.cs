using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement = Vector2.zero;

    private float xDir = 0, yDir = 0;
    [SerializeField] private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateInput();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        movement = new Vector2(xDir, yDir).normalized;
        rb.velocity = movement * speed;
        animator.SetFloat("xSpeed", xDir);
        animator.SetFloat("ySpeed", yDir);
    }

    private void UpdateInput()
    {
        xDir = Input.GetAxisRaw("Horizontal");
        yDir = Input.GetAxisRaw("Vertical");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);
    }
}
