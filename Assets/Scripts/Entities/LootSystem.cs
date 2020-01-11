using UnityEngine;

public static class LootSystem
{
    private static LootItemHolder _lootItemHolderPrefab;
    
    public static void Drop(Item item, Transform droppingTransform)
    {
        LootItemHolder lootItemHolder = GetLootItemHolder();
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

    private static LootItemHolder GetLootItemHolder()
    {
        if (_lootItemHolderPrefab == null)
        {
            _lootItemHolderPrefab = Resources.Load<LootItemHolder>("LootItemHolder");
        }
        LootItemHolder lootItemHolder = GameObject.Instantiate(_lootItemHolderPrefab);
        return lootItemHolder;
    }
}