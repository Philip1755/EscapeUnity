using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Vector2 openDir;
    [SerializeField] private float speed = 0.2f;

    private Vector2 closePosition,openPosition;

    private void Start()
    {
        closePosition = transform.position;
        openPosition = closePosition + openDir;
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
        float time = 0;

        if (openDir.x != 0)
        {
            time = 1-(openPosition.x - transform.position.x) / openDir.x;
        }
        else if (openDir.y != 0) 
        {
            time = 1-(openPosition.y - transform.position.y) / openDir.y;
        }

        while (time <= 1)
        {
            transform.position = Vector2.Lerp(closePosition, openPosition, time);

            yield return null;

            time += Time.deltaTime * speed;
        }

    }

    private IEnumerator Close()
    {
        float time = 0;

        if (openDir.x != 0)
        {
            time = 1+(closePosition.x - transform.position.x) / openDir.x;
        }
        else if (openDir.y != 0)
        {
            time = 1+(closePosition.y - transform.position.y) / openDir.y;
        }

        while (time <= 1)
        {
            transform.position = Vector2.Lerp(openPosition, closePosition, time);

            yield return null;

            time += Time.deltaTime * speed;
        }

    }
}
