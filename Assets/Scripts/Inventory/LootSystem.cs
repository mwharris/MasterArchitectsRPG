using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LootSystem : MonoBehaviour
{
    [SerializeField] private AssetReference _lootItemHolderPrefab;
    
    private static LootSystem _instance;
    private static Queue<LootItemHolder> _lootItemHolders = new Queue<LootItemHolder>();

    private void Awake()
    {
        // Make sure there's only ever 1 LootSystem
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public static void Drop(Item item, Transform droppingTransform)
    {
        // If we have LootItemHolders in our Object Pool
        if (_lootItemHolders.Any())
        {
            Debug.Log("Dequeuing a LootItemHolder");
            // Grab a reference to one
            LootItemHolder lootItemHolder = _lootItemHolders.Dequeue();
            // Reactivate itB
            lootItemHolder.gameObject.SetActive(true);
            // Setup this reused LootItemHolder 
            AssignLootItemHolder(lootItemHolder, item, droppingTransform);
        }
        else
        {
            Debug.Log("Instantiating a LootItemHolder");
            _instance.StartCoroutine(_instance.DropAsync(item, droppingTransform));
        }
    }

    private IEnumerator DropAsync(Item item, Transform droppingTransform)
    {
        AsyncOperationHandle<GameObject> operation = _lootItemHolderPrefab.InstantiateAsync();
        yield return operation;
        
        LootItemHolder lootItemHolder = operation.Result.GetComponent<LootItemHolder>();;
        AssignLootItemHolder(lootItemHolder, item, droppingTransform);
    }

    private static void AssignLootItemHolder(LootItemHolder lootItemHolder, Item item, Transform droppingTransform)
    {
        lootItemHolder.SetItem(item);
        lootItemHolder.transform.position = GetRandomPosition(droppingTransform);
    }

    private static Vector3 GetRandomPosition(Transform droppingTransform)
    {
        // Pick a random point inside a circle with radius 3 around or dropping transform (2D)
        Vector2 randomCirclePoint = UnityEngine.Random.insideUnitCircle * 3;
        // Map that 2D point to 3D space to get our random position
        Vector3 randomPosition = droppingTransform.position + new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);
        return randomPosition;
    }

    public static void AddToPool(LootItemHolder lootItemHolder)
    {
        lootItemHolder.gameObject.SetActive(false);
        _lootItemHolders.Enqueue(lootItemHolder);
    }
}