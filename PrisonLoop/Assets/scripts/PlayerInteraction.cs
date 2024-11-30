using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private KeyCode interactionKey = KeyCode.E; // Klawisz interakcji

    private IInteractable currentInteractable;

    private void Update()
    {
        // SprawdŸ, czy gracz naciska klawisz interakcji
        if (currentInteractable != null && Input.GetKeyDown(interactionKey))
        {
            currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SprawdŸ, czy obiekt posiada komponent implementuj¹cy IInteractable
        IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            interactable.OnPlayerEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Usuñ obiekt z obecnego kontekstu, gdy gracz opuœci jego zasiêg
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && interactable == currentInteractable)
        {
            interactable.OnPlayerExit();
            currentInteractable = null;
        }
    }
}
