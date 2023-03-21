using UnityEngine;

public class EnemyController : MonoBehaviour, IHitable
{
    [SerializeField] private AudioClip dead;
    [SerializeField] private int life = 100;

    public void TakeDamage(int damage)
    {
        life -= damage;
        if(life <= 0)
        {
            AudioManager.Instance.PlaySoundEffect(dead);
            Destroy(gameObject);
        }
    } 
}
