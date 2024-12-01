using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FoodSpawner : MiniGameBase
{
    [Header("Food Settings")]
    [SerializeField] private List<GameObject> foodPrefabs;
    [SerializeField] private int initialSpawnCount = 10; 
    [SerializeField] private Transform spawnArea; 

    private List<GameObject> activeFoods = new List<GameObject>();
    
    private void Start()
    {
        onMiniGameStart?.Invoke();
        SpawnInitialFoods();
    }

    private void SpawnInitialFoods()
    {
        if (foodPrefabs == null || foodPrefabs.Count == 0)
        {
            Debug.LogError("Brak prefabów Food w liście!");
            return;
        }

        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnFood();
        }
    }

    private void SpawnFood()
    {
        GameObject randomFoodPrefab = foodPrefabs[Random.Range(0, foodPrefabs.Count)];
        Vector3 spawnPosition = GetRandomPositionInArea();
        GameObject foodInstance = Instantiate(randomFoodPrefab, spawnPosition, Quaternion.identity);
        activeFoods.Add(foodInstance);
        
        foodInstance.GetComponentInChildren<Food>().OnDestroyCallback = () =>
        {
            activeFoods.Remove(foodInstance);
            if (activeFoods.Count == 0)
            {
                
                onMiniGameEnd?.Invoke();
                Destroy(gameObject);
            }
        };
    }

    private Vector3 GetRandomPositionInArea()
    {
        if (spawnArea == null)
        {
            Debug.LogError("Nie ustawiono obszaru generowania (spawnArea)!");
            return Vector3.zero;
        }
        Vector3 areaCenter = spawnArea.position;
        Vector3 areaSize = spawnArea.localScale;
        
        float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float randomY = Random.Range(-areaSize.y / 2, areaSize.y / 2);
        
        Vector3 spawnPosition = areaCenter + new Vector3(randomX, randomY, 0f);
        return spawnPosition;
    }
}
