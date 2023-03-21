using UnityEngine;

public class KeyController : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip collectKey;
    [SerializeField] private int keyID;

    public void Interact(GameObject interactor)
    {
        interactor.GetComponent<PlayerController>()?.AddKey(keyID);
        AudioManager.Instance.PlaySoundEffect(collectKey);
        Destroy(gameObject);
    }
}
