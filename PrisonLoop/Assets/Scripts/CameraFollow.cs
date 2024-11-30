using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform player; // Obiekt gracza
    [SerializeField] private float smoothSpeed = 0.125f; // Szybkoœæ wyg³adzania
    [SerializeField] private Vector3 offset=new Vector3(0,0,-10); // Offset kamery od gracza
    private Vector3 velocity = Vector3.zero;
    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed); ;

        transform.position = smoothedPosition;
    }
}
