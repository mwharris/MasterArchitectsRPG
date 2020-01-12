using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LootSystem : MonoBehaviour
{
    [SerializeField] private AssetReference _lootItemHolderPrefab;
    
    private static LootSystem _instance;

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
        _instance.StartCoroutine(_instance.DropAsync(item, droppingTransform));
    }

    private IEnumerator DropAsync(Item item, Transform droppingTransform)
    {
        AsyncOperationHandle<GameObject> operation = _lootItemHolderPrefab.InstantiateAsync();
        yield return operation;
        
        LootItemHolder lootItemHolder = operation.Result.GetComponent<LootItemHolder>();;
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
}