using UnityEngine;

public class KeyController : MonoBehaviour, IInteractable
{
    [SerializeField] private int keyID;

    public void Interact(GameObject interactor)
    {
        Debug.Log("Picked Up Key");
        interactor.GetComponent<PlayerController>()?.AddKey(keyID);
        Destroy(gameObject);
    }
}
