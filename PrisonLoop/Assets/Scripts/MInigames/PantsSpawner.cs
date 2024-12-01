using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using MInigames;

public class PantsSpawner : MiniGameBase
{
    [Header("Spawn Settings")]
    [SerializeField] private List<GameObject> pantsPrefabs; // Prefab obiektu ko³a
    [SerializeField] private float spawnInterval = 1.0f; // Odstêp czasu miêdzy spawnami

    public void onEnd()
    {
        onMiniGameEnd?.Invoke();
        Destroy(gameObject);
    }
    private GameObject[] items;
    private void Start()
    {
        onMiniGameStart?.Invoke();
    }
    private float spawnTimer=0;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnCircle();
            spawnTimer = 0f;
        }
    }

    private void SpawnCircle()
    {
        float randomX = Random.Range(-8, 8);
        Vector3 spawnPosition = new Vector3(randomX, 5f, 0);

        int randomIdx = Random.Range(0, 5);

        GameObject newCircle = Instantiate(pantsPrefabs[randomIdx], spawnPosition, Quaternion.identity, gameObject.transform);

        // Ustawienie Sorting Layer i Order in Layer
        SpriteRenderer spriteRenderer = newCircle.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = "Minigame"; // Nazwa warstwy
            spriteRenderer.sortingOrder = 3;             // Kolejnoœæ w warstwie
        }
      /*  BoxCollider2D boxCollider = newCircle.AddComponent<BoxCollider2D>();

        boxCollider.isTrigger = false;
        boxCollider.size = spriteRenderer.bounds.size;*/

        //newCircle.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnEnable()
    {
        BacketMovement.OnSignalSent += HandleSignal;
    }

    private void OnDisable()
    {
        BacketMovement.OnSignalSent -= HandleSignal;
    }

    private void HandleSignal()
    {
        onMiniGameEnd?.Invoke();
        Destroy(gameObject);
    }
}
