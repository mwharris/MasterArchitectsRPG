using System;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class NPCLoot : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private Item[] _itemPrefabs;
    
    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }

    private void Start()
    {
        foreach (var itemPrefab in _itemPrefabs)
        {
            Item itemInstance = Instantiate(itemPrefab);
            _inventory.Pickup(itemInstance);
        }
    }
}