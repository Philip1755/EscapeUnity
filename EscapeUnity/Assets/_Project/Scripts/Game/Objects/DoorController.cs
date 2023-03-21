using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Vector2 openDir;
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private bool locked = false;
    [SerializeField] private bool needsKey = false;
    [SerializeField] private int targetKeyID;

    private Vector2 closePosition, openPosition;

    private void Start()
    {
        closePosition = transform.position;
        openPosition = closePosition + openDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (needsKey)
            locked = PlayerHasTargetKey(collision.gameObject.GetComponent<PlayerController>()?.GetKeys());


        if (locked) return;

        StopAllCoroutines();
        StartCoroutine(Open());
    }

    private bool PlayerHasTargetKey(List<int> list)
    {
        foreach (int id in list)
            if (id == targetKeyID) return false;

        return true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (locked) return;

        StopAllCoroutines();
        StartCoroutine(Close());
    }

    private IEnumerator Open()
    {
        float time = 0;

        if (openDir.x != 0)
        {
            time = 1 - (openPosition.x - transform.position.x) / openDir.x;
        }
        else if (openDir.y != 0)
        {
            time = 1 - (openPosition.y - transform.position.y) / openDir.y;
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
            time = 1 + (closePosition.x - transform.position.x) / openDir.x;
        }
        else if (openDir.y != 0)
        {
            time = 1 + (closePosition.y - transform.position.y) / openDir.y;
        }

        while (time <= 1)
        {
            transform.position = Vector2.Lerp(openPosition, closePosition, time);

            yield return null;

            time += Time.deltaTime * speed;
        }

    }
}
