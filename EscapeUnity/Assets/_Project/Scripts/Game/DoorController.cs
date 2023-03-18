using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Vector2 openDir;
    public float speed = 1f;
    private bool isOpen = false;
    private Vector2 pos;
    public Sprite x;
    public Sprite y;
    public SpriteRenderer spRender;

    public void Awake()
    {
        pos = transform.position;
        if(Mathf.Abs(openDir.x) > 0)
        {
            spRender.sprite = x;
        }
        else
        {
            spRender.sprite = y;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpen)
        {
            Debug.Log("OPEN");
            openDoor();
        }
        
    }

    private void openDoor()
    {
        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + openDir;

        /**float time = 0;
        isOpen = true;
        while (time < 1)
        {
            transform.position = Vector2.Lerp(startPos, endPos, time);
            time += Time.deltaTime * speed;
        }*/

        isOpen = true;
        transform.position = endPos;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isOpen)
        {
            Debug.Log("CLOSE");
            closeDoor();
        }
    }

    private void closeDoor()
    {
        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + -openDir;

        /**float time = 0;
        isOpen = false;
        while (time < 1)
        {
            transform.position = Vector2.Lerp(startPos, endPos, time);
            time += Time.deltaTime * speed;
        }*/

        isOpen = false;
        transform.position = endPos;
    }
}
