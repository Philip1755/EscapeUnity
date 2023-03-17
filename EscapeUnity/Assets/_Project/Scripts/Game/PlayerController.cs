using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement = Vector2.zero;
    private float xDir = 0, yDir = 0;

    [Header("Movement")]
    [SerializeField] private float speed = 2;

    [Header("Melee Combat")]
    [SerializeField] private AudioClip punch;
    [SerializeField] private LayerMask hitableMask;
    [SerializeField] private GameObject attackPointLeft, attackPointRight, attackPointDown, attackPointUp;
    [SerializeField] private float hitRadius = .3f;
    [SerializeField] private float basePunchSpeed = 0.2f;

    private bool hitLeft, hitDown, hitRight, hitUp;
    private float punchTimer = 0;

    [Header("Interact System")]
    [SerializeField] private GameObject eKeyUI;

    [SerializeField] private KeyCode interactKey;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private float interactionRadius = .8f;

    private List<GameObject> interactables;
    private bool canInteract;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateInput();
        UpdateMeleeCombat();
        CheckForInteractables();

//        transform.RotateToPoint2D(Utility.GetMouseWorldPosition2D(), 90f);
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateInput()
    {
        if (PauseMenuManager.isPaused)
            return;

        xDir = Input.GetAxisRaw("Horizontal");
        yDir = Input.GetAxisRaw("Vertical");

        hitLeft = Input.GetMouseButtonDown(0) && Utility.GetMouseWorldPosition2D().x < transform.position.x &&
            Mathf.Abs(Utility.GetMouseWorldPosition2D().y - transform.position.y) < .5;
        hitRight = Input.GetMouseButtonDown(0) && Utility.GetMouseWorldPosition2D().x > transform.position.x &&
            Mathf.Abs(Utility.GetMouseWorldPosition2D().y - transform.position.y) < .5;
        hitDown = Input.GetMouseButtonDown(0) && Utility.GetMouseWorldPosition2D().y < transform.position.y &&
            Mathf.Abs(Utility.GetMouseWorldPosition2D().x - transform.position.x) < .5;
        hitUp = Input.GetMouseButtonDown(0) && Utility.GetMouseWorldPosition2D().y > transform.position.y &&
            Mathf.Abs(Utility.GetMouseWorldPosition2D().x - transform.position.x) < .5;

        canInteract = Input.GetKeyDown(interactKey);
    }

    private void UpdateMovement()
    {
        movement = new Vector2(xDir, yDir).normalized;
        rb.velocity = movement * speed;
        animator.SetFloat("xSpeed", xDir);
        animator.SetFloat("ySpeed", yDir);
    }

    private void UpdateMeleeCombat()
    {
        if(punchTimer > 0)
        {
            punchTimer -= Time.deltaTime;
            return;
        }

        if (IsMoving()) return;

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

        if (hitUp)
        {
            animator.SetTrigger("hitUp");
            hittedObjects = Utility.CheckForGameObjects2D(attackPointUp.transform.position, hitRadius, hitableMask);
        }

        if (hitLeft || hitDown || hitRight || hitUp)
        {
            AudioManager.Instance.PlaySoundEffect(punch);
            punchTimer = basePunchSpeed;
        }

        if (hittedObjects == null) return;

        foreach (var obj in hittedObjects)
            Debug.Log("Hitted: " + obj.name);
    }

    private bool IsMoving() => !(xDir == 0 && yDir == 0);

    private void CheckForInteractables()
    {
        interactables = Utility.CheckForGameObjects2D(transform.position, interactionRadius, interactableMask);

        eKeyUI.SetActive(false);

        if (interactables == null || interactables.Count <= 0) return;

        eKeyUI.SetActive(true);

        if (canInteract)
            interactables[0].GetComponent<IInteractable>()?.Interact(gameObject);
    }

    private void OnDrawGizmos()
    {
        //Melee attack Points
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPointDown.transform.position, hitRadius);
        Gizmos.DrawWireSphere(attackPointLeft.transform.position, hitRadius);
        Gizmos.DrawWireSphere(attackPointRight.transform.position, hitRadius);
        Gizmos.DrawWireSphere(attackPointUp.transform.position, hitRadius);

        //Interact System
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}