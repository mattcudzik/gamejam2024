using UnityEngine;

public interface IInteractable
{
    // Metoda wywo³ywana, gdy gracz wchodzi w interakcjê z obiektem
    void Interact();

    // Opcjonalna metoda, gdy gracz znajdzie siê w zasiêgu interakcji
    void OnPlayerEnter();

    // Opcjonalna metoda, gdy gracz opuœci zasiêg interakcji
    void OnPlayerExit();
}
