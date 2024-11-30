using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f; // Pr�dko�� poruszania

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        // Pobranie komponentu Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Brak komponentu Rigidbody2D na obiekcie!");
        }
    }

    private void Update()
    {
        // Odczyt osi wej�ciowych
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Tworzenie wektora ruchu
        movement = new Vector2(horizontal, vertical).normalized;
    }

    private void FixedUpdate()
    {
        // Poruszanie gracza
        rb.linearVelocity = movement * moveSpeed;
    }
}