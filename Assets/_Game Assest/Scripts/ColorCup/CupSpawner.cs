using System.Collections.Generic;
using UnityEngine;

public class CupSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjectPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> cupListTransforms = new List<Transform>();
    [SerializeField] private List<Transform> cupListInvisibleTransforms = new List<Transform>();
    [SerializeField] private MixEffectController mixEffectController;
    [SerializeField] private MatchCounter matchCounter;
    [SerializeField] private ObjectMover objectMover;


    private List<GameObject> spawnedInvisibleObjects = new List<GameObject>();
    private List<GameObject> selectedPrefabs = new List<GameObject>();

    private int shuffleCount = 0;
    private const int maxShuffles = 3;
    [SerializeField] private float spawnRate;
    private void Awake()
    {
        objectMover.IsMoveActive(false);;
        if (ValidateInput())
        {
            selectedPrefabs = SelectRandomItems(gameObjectPrefabs, 4);
            List<Transform> selectedSpawnPoints = SelectRandomItems(cupListTransforms, 4);
            List<Transform> selectedInvisibleSpawnPoints = SelectRandomItems(cupListInvisibleTransforms, 4);

            SpawnPrefabs(selectedPrefabs, selectedSpawnPoints);
            SpawnInvisiblePrefabs(selectedPrefabs, selectedInvisibleSpawnPoints);

            InvokeRepeating(nameof(ShuffleInvisibleObjects), spawnRate, spawnRate);
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
            spawnedObject.GetComponent<Rigidbody>().isKinematic = true;
            spawnedInvisibleObjects.Add(spawnedObject);
        }
    }

    private void ShuffleInvisibleObjects()
    {
        // Var olan objeleri yok etmek yerine, pozisyonlarını değiştiriyoruz
        mixEffectController.StartMixEffect();

        List<Transform> newSpawnPoints = SelectRandomItems(cupListInvisibleTransforms, 4);
        for (int i = 0; i < spawnedInvisibleObjects.Count; i++)
        {
            spawnedInvisibleObjects[i].transform.position = newSpawnPoints[i].position;
            spawnedInvisibleObjects[i].transform.SetParent(newSpawnPoints[i]);
        }

        shuffleCount++;
        if (shuffleCount >= maxShuffles)
        {
            CancelInvoke(nameof(ShuffleInvisibleObjects));
            mixEffectController.EndMixEffect();

            foreach (var comparer in matchCounter.cupComparers)
            {
                comparer.InitializeCounting();
            }
            matchCounter.InitializeGame();
            objectMover.IsMoveActive(true);
        }
    }



    //destroy edip yeniden spawn ediyorduk bu versiyonda simdiki versiyonda destroy yerine yer degistiriyoruz
    //private void ShuffleInvisibleObjects()
    //{
    //    foreach (GameObject obj in spawnedInvisibleObjects)
    //    {
    //        Destroy(obj);
    //    }
    //    spawnedInvisibleObjects.Clear();

    //    mixEffectController.StartMixEffect();

    //    List<Transform> newSpawnPoints = SelectRandomItems(cupListInvisibleTransforms, 4);
    //    SpawnInvisiblePrefabs(selectedPrefabs, newSpawnPoints);

    //    shuffleCount++;
    //    if (shuffleCount >= maxShuffles)
    //    {
    //        CancelInvoke(nameof(ShuffleInvisibleObjects));
    //        mixEffectController.EndMixEffect();

    //        foreach (var comparer in matchCounter.cupComparers)
    //        {
    //            comparer.InitializeCounting();
    //        }
    //        matchCounter.InitializeGame();
    //        objectMover.IsMoveActive(true);
    //    }
    //}
}

