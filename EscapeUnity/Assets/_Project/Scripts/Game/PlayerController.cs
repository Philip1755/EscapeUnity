using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement = Vector2.zero;

    private float xDir = 0, yDir = 0;
    [SerializeField] private float speed = 2;

    [SerializeField] private LayerMask hitableMask;
    [SerializeField] private GameObject attackPointLeft, attackPointRight, attackPointDown;
    [SerializeField] private float hitRadius = .3f;
    private bool hitLeft, hitDown, hitRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateInput();
        UpdateMeleeCombat();
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

        hitLeft = Input.GetMouseButtonDown(0) && Utility.GetMouseWorldPosition2D().x < transform.position.x &&
            Mathf.Abs(Utility.GetMouseWorldPosition2D().y - transform.position.y) < .5;
        hitRight = Input.GetMouseButtonDown(0) && Utility.GetMouseWorldPosition2D().x > transform.position.x &&
            Mathf.Abs(Utility.GetMouseWorldPosition2D().y - transform.position.y) < .5;
        hitDown = Input.GetMouseButtonDown(0) && Utility.GetMouseWorldPosition2D().y < transform.position.y &&
            Mathf.Abs(Utility.GetMouseWorldPosition2D().x - transform.position.x) < .5;
    }

    private void UpdateMeleeCombat()
    {
        if (!CanHit()) return;

        List<GameObject> hittedObjects = null;

        if (hitLeft)
        {
            animator.SetTrigger("hitLeft");
            hittedObjects = Utility.CheckForGameObjects2D(attackPointLeft.transform.position, hitRadius, hitableMask);
        }

        if (hitRight)
        {
            animator.SetTrigger("hitRight");
            hittedObjects = Utility.CheckForGameObjects2D(attackPointRight.transform.position, hitRadius, hitableMask);
        }

        if (hitDown)
        {
            animator.SetTrigger("hitDown");
            hittedObjects = Utility.CheckForGameObjects2D(attackPointDown.transform.position, hitRadius, hitableMask);
        }

        if (hittedObjects == null) return;

        foreach (var obj in hittedObjects)
            Debug.Log("Hitted: " + obj.name);
    }

    private bool CanHit() => xDir == 0 && yDir == 0;

    private void OnDrawGizmos()
    {
        //Melee attack Points
        Gizmos.DrawWireSphere(attackPointDown.transform.position, hitRadius);
        Gizmos.DrawWireSphere(attackPointLeft.transform.position, hitRadius);
        Gizmos.DrawWireSphere(attackPointRight.transform.position, hitRadius);
    }
}