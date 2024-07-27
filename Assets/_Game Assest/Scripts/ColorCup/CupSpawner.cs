using System.Collections.Generic;
using UnityEngine;

public class CupSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjectPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> cupListTransforms = new List<Transform>();
    [SerializeField] private List<Transform> cupListInvisibleTransforms = new List<Transform>();
    [SerializeField] private MixEffectController mixEffectController;

    private List<GameObject> spawnedInvisibleObjects = new List<GameObject>();
    private List<GameObject> selectedPrefabs = new List<GameObject>();
    private int shuffleCount = 0;
    private const int maxShuffles = 3;

    private void Awake()
    {
        if (ValidateInput())
        {
            selectedPrefabs = SelectRandomItems(gameObjectPrefabs, 4);
            List<Transform> selectedSpawnPoints = SelectRandomItems(cupListTransforms, 4);
            List<Transform> selectedInvisibleSpawnPoints = SelectRandomItems(cupListInvisibleTransforms, 4);

            SpawnPrefabs(selectedPrefabs, selectedSpawnPoints);
            SpawnInvisiblePrefabs(selectedPrefabs, selectedInvisibleSpawnPoints);

            InvokeRepeating(nameof(ShuffleInvisibleObjects), .5f, 3);
        }
    }

    private bool ValidateInput()
    {
        if (gameObjectPrefabs.Count < 4 || cupListTransforms.Count < 4 || cupListInvisibleTransforms.Count < 4)
        {
            Debug.LogError("Prefabs or SpawnPoints count is less than 4!");
            return false;
        }
        return true;
    }

    private List<T> SelectRandomItems<T>(List<T> items, int count)
    {
        List<T> selectedItems = new List<T>();
        List<int> availableIndices = new List<int>();

        for (int i = 0; i < items.Count; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            selectedItems.Add(items[availableIndices[randomIndex]]);
            availableIndices.RemoveAt(randomIndex);
        }

        return selectedItems;
    }

    private void SpawnPrefabs(List<GameObject> prefabs, List<Transform> spawnPoints)
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            Instantiate(prefabs[i], spawnPoints[i].position, Quaternion.identity, spawnPoints[i]);
        }
    }

    private void SpawnInvisiblePrefabs(List<GameObject> prefabs, List<Transform> spawnPoints)
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            GameObject spawnedObject = Instantiate(prefabs[i], spawnPoints[i].position, Quaternion.identity, spawnPoints[i]);
            spawnedInvisibleObjects.Add(spawnedObject);
        }
    }

    private void ShuffleInvisibleObjects()
    {
        foreach (GameObject obj in spawnedInvisibleObjects)
        {
            Destroy(obj);
        }
        spawnedInvisibleObjects.Clear();

        mixEffectController.StartMixEffect();

        List<Transform> newSpawnPoints = SelectRandomItems(cupListInvisibleTransforms, 4);
        SpawnInvisiblePrefabs(selectedPrefabs, newSpawnPoints);

        shuffleCount++;
        if (shuffleCount >= maxShuffles)
        {
            CancelInvoke(nameof(ShuffleInvisibleObjects));
            mixEffectController.EndMixEffect();
        }
    }
}







//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CupSpawner : MonoBehaviour
//{
//    [SerializeField] private List<GameObject> gameObjectPrefab = new List<GameObject>();
//    [SerializeField] private List<Transform> cupListTransform = new List<Transform>();
//    [SerializeField] private List<Transform> cupListInvisibleTransform = new List<Transform>();

//    private void Awake()
//    {
//        SpawnRandomPrefabs();
//    }

//    void SpawnRandomPrefabs()
//    {
//        if (gameObjectPrefab.Count < 4 || cupListTransform.Count < 4)
//        {
//            Debug.LogError("Prefabs or SpawnPoints count is less than 4!");
//            return;
//        }

//        // Prefab ve SpawnPoint index listelerini oluştur
//        List<int> prefabIndices = new List<int>();
//        List<int> spawnPointIndices = new List<int>();
//        List<int> spawnPointIndicesInvisible = new List<int>();


//        for (int i = 0; i < gameObjectPrefab.Count; i++)
//        {
//            prefabIndices.Add(i);
//        }

//        for (int i = 0; i < cupListTransform.Count; i++)
//        {
//            spawnPointIndices.Add(i);
//        }
//        for (int i = 0; i < cupListInvisibleTransform.Count; i++)
//        {
//            spawnPointIndicesInvisible.Add(i);
//        }

//        // Prefab ve SpawnPoint listelerinden random 4 tanesini seç
//        List<GameObject> selectedPrefabs = new List<GameObject>();
//        List<Transform> selectedSpawnPoints = new List<Transform>();
//        List<Transform> selectedInvisibleSpawnPoints = new List<Transform>();

//        for (int i = 0; i < 4; i++)
//        {
//            int randomPrefabIndex = Random.Range(0, prefabIndices.Count);
//            selectedPrefabs.Add(gameObjectPrefab[prefabIndices[randomPrefabIndex]]);
//            prefabIndices.RemoveAt(randomPrefabIndex);

//            int randomSpawnPointIndex = Random.Range(0, spawnPointIndices.Count);
//            selectedSpawnPoints.Add(cupListTransform[spawnPointIndices[randomSpawnPointIndex]]);
//            spawnPointIndices.RemoveAt(randomSpawnPointIndex);

//            int randomInvisibleSpawnPointIndex = Random.Range(0, spawnPointIndicesInvisible.Count);
//            selectedInvisibleSpawnPoints.Add(cupListInvisibleTransform[spawnPointIndicesInvisible[randomInvisibleSpawnPointIndex]]);
//            spawnPointIndicesInvisible.RemoveAt(randomInvisibleSpawnPointIndex);
//        }

//        // Seçilen prefabları random seçilen spawn noktalarına yerleştir
//        for (int i = 0; i < selectedPrefabs.Count; i++)
//        {
//            Instantiate(selectedPrefabs[i], selectedSpawnPoints[i].position, Quaternion.identity, selectedSpawnPoints[i]);

//        }
//        for (int i = 0; i < selectedPrefabs.Count; i++)
//        {
//            Instantiate(selectedPrefabs[i], selectedInvisibleSpawnPoints[i].position, Quaternion.identity,selectedInvisibleSpawnPoints[i]);
//        }
//    }


//}
