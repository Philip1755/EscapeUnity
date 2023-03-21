using UnityEngine;

public class ShowerFightEvent : MonoBehaviour
{
    [SerializeField] private EnemyController[] enemies;
    [SerializeField] private AudioClip eventStart;

    private PlayerController player;

    private int currentEnemyIndex = 0;

    private void Update()
    {
        if (currentEnemyIndex < enemies.Length - 1 && enemies[currentEnemyIndex] == null)
            enemies[++currentEnemyIndex].gameObject.SetActive(true);

        if (enemies[enemies.Length - 1] == null)
        {
            player.AddKey(2);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.PlaySoundEffect(eventStart);
        player = collision.GetComponent<PlayerController>();
        enemies[currentEnemyIndex].gameObject.SetActive(true);
        GetComponent<Collider2D>().enabled = false;
    }
}
