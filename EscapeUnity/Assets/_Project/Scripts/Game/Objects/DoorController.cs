using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Sprite xSprite, ySprite;
    [SerializeField] private Vector2 openDir;
    [SerializeField] private float speed = 0.2f;

    private SpriteRenderer spriteRenderer;
    private Vector2 closePosition;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        closePosition = transform.position;

        spriteRenderer.sprite = openDir.x == 0 ? ySprite : xSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopAllCoroutines();
        StartCoroutine(Open());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
        StartCoroutine(Close());
    }

    private IEnumerator Open()
    {
        Vector2 startPos = transform.position;
        Vector2 endPos = closePosition + openDir;

        float time = 0;

        while (time <= 1)
        {
            transform.position = Vector2.Lerp(startPos, endPos, time);

            yield return null;

            time += Time.deltaTime * speed;
        }

    }

    private IEnumerator Close()
    {
        Vector2 startPos = transform.position;
        Vector2 endPos = closePosition;

        float time = 0;

        while (time <= 1)
        {
            transform.position = Vector2.Lerp(startPos, endPos, time);

            yield return null;

            time += Time.deltaTime * speed;
        }

    }
}
