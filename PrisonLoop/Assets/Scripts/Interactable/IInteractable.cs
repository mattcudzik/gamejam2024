using UnityEngine;

public interface IInteractable
{
    // Metoda wywo�ywana, gdy gracz wchodzi w interakcj� z obiektem
    void Interact();

    // Opcjonalna metoda, gdy gracz znajdzie si� w zasi�gu interakcji
    void OnPlayerEnter();

    // Opcjonalna metoda, gdy gracz opu�ci zasi�g interakcji
    void OnPlayerExit();
}
