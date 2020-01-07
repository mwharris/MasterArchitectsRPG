using System;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class NPCLoot : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private Item[] _itemPrefabs;
    private EntityStateMachine _entityStateMachine;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _entityStateMachine = GetComponent<EntityStateMachine>();
    }

    private void Start()
    {
        _entityStateMachine.OnEntityStateChanged += HandleEntityStateChanged;
        
        foreach (var itemPrefab in _itemPrefabs)
        {
            Item itemInstance = Instantiate(itemPrefab);
            _inventory.Pickup(itemInstance);
        }
    }

    private void HandleEntityStateChanged(IState state)
    {
        if (state is Dead)
        {
            DropLoot();
        }
    }

    private void DropLoot()
    {
        Debug.Log("Dropping loot...");
        foreach (var item in _inventory.Items)
        {
            item.transform.SetParent(null);
            item.transform.position = transform.position + transform.right;
            item.gameObject.SetActive(true);
        }
        _inventory.Items.Clear();
    }
}