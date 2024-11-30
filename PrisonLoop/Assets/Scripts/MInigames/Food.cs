using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Food : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 1f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip clickSound;

    
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    
    
    public System.Action OnDestroyCallback;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movementDirection;
    private float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        SetRandomColor();
        SetRandomVelocity();
    }

    private void SetRandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        spriteRenderer.color = randomColor;
    }
    private void SetRandomVelocity()
    {
        speed = Random.Range(minSpeed, maxSpeed); 
        movementDirection = Random.insideUnitCircle.normalized;
        rb.linearVelocity = movementDirection * speed;
    }

    private void OnMouseDown()
    {
        if (clickSound != null)
        {
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        }
        Destroy(gameObject);
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
    private void OnDestroy()
    {
        OnDestroyCallback?.Invoke();
    }
}