using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private GameObject eKeyUI;

    [SerializeField] private KeyCode InteractKey;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private float interactionRadius = .8f;

    private List<GameObject> interactables;

    private void Update()
    {
        interactables = Utility.CheckForGameObjects2D(transform.position, interactionRadius, interactableMask);

        eKeyUI.SetActive(false);

        if (interactables == null || interactables.Count <= 0) return;

        eKeyUI.SetActive(true);

        if(Input.GetKeyDown(InteractKey))
            interactables[0].GetComponent<IInteractable>()?.Interact(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
