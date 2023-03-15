using UnityEngine;

public class BedController : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interactor)
    {
        Debug.Log("Sleep");
    }
}
