using TMPro;
using UnityEngine;

public class BacketMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f; // Prêdkoœæ poruszania siê
    [SerializeField] private int gameEnd = 10; // Prêdkoœæ poruszania siê

    private Rigidbody2D rb;
    public delegate void SignalAction();
    public static event SignalAction OnSignalSent;
    public TextMeshProUGUI resultText;
    public void TriggerSignal()
    {
        OnSignalSent?.Invoke();
    }
    private void Start()
    {
        // Pobranie komponentu Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Brak komponentu Rigidbody2D na obiekcie!");
        }
        updadeResultStr();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Odczytujemy dane z poziomych osi (klawisze A/D, strza³ki lewo/prawo)

        rb.linearVelocityX = horizontalInput * moveSpeed;
    }
    private int caught = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        caught++;
        updadeResultStr();
        if (caught == gameEnd)
        {
            TriggerSignal();
        }
    }

    void updadeResultStr()
    {
        resultText.text = caught+"/"+gameEnd;
    }

}
