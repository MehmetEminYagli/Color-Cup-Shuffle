using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjectPrefab = new List<GameObject>();
    [SerializeField] private List<Transform> cupListTransform = new List<Transform>();
    [SerializeField] private List<Transform> cupListInvisibleTransform = new List<Transform>();
    void Start()
    {
        SpawnRandomPrefabs();
    }

    void SpawnRandomPrefabs()
    {
        if (gameObjectPrefab.Count < 4 || cupListTransform.Count < 4)
        {
            Debug.LogError("Prefabs or SpawnPoints count is less than 4!");
            return;
        }

        // Prefab ve SpawnPoint index listelerini oluştur
        List<int> prefabIndices = new List<int>();
        List<int> spawnPointIndices = new List<int>();
        List<int> spawnPointIndicesInvisible = new List<int>();


        for (int i = 0; i < gameObjectPrefab.Count; i++)
        {
            prefabIndices.Add(i);
        }

        for (int i = 0; i < cupListTransform.Count; i++)
        {
            spawnPointIndices.Add(i);
        }
        for (int i = 0; i < cupListInvisibleTransform.Count; i++)
        {
            spawnPointIndicesInvisible.Add(i);
        }

        // Prefab ve SpawnPoint listelerinden random 4 tanesini seç
        List<GameObject> selectedPrefabs = new List<GameObject>();
        List<Transform> selectedSpawnPoints = new List<Transform>();
        List<Transform> selectedInvisibleSpawnPoints = new List<Transform>();

        for (int i = 0; i < 4; i++)
        {
            int randomPrefabIndex = Random.Range(0, prefabIndices.Count);
            selectedPrefabs.Add(gameObjectPrefab[prefabIndices[randomPrefabIndex]]);
            prefabIndices.RemoveAt(randomPrefabIndex);

            int randomSpawnPointIndex = Random.Range(0, spawnPointIndices.Count);
            selectedSpawnPoints.Add(cupListTransform[spawnPointIndices[randomSpawnPointIndex]]);
            spawnPointIndices.RemoveAt(randomSpawnPointIndex);

            int randomInvisibleSpawnPointIndex = Random.Range(0, spawnPointIndicesInvisible.Count);
            selectedInvisibleSpawnPoints.Add(cupListInvisibleTransform[spawnPointIndicesInvisible[randomInvisibleSpawnPointIndex]]);
            spawnPointIndicesInvisible.RemoveAt(randomInvisibleSpawnPointIndex);
        }

        // Seçilen prefabları random seçilen spawn noktalarına yerleştir
        for (int i = 0; i < selectedPrefabs.Count; i++)
        {
            Instantiate(selectedPrefabs[i], selectedSpawnPoints[i].position, Quaternion.identity, selectedSpawnPoints[i]);

        }
        for (int i = 0; i < selectedPrefabs.Count; i++)
        {
            Instantiate(selectedPrefabs[i], selectedInvisibleSpawnPoints[i].position, Quaternion.identity,selectedInvisibleSpawnPoints[i]);
        }
    }
}
