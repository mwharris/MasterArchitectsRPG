using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<Item> ActiveItemChanged;
    public event Action<Item> ItemPickedUp;
    
    [SerializeField] private Transform _rightHand;
    private Transform _itemRoot;
    
    private List<Item> _items = new List<Item>();
    public List<Item> Items => _items;
    
    public Item ActiveItem { get; private set; }
    
    void Awake()
    {
        // Create our item root as a child of this game object
        _itemRoot = new GameObject("Items").transform;
        _itemRoot.transform.SetParent(transform);
    }

    public void Pickup(Item item)
    {
        // Add the item to our list of items
        _items.Add(item);
        // Child it to this game object
        item.transform.parent = _itemRoot;
        // Equip this item
        Equip(item);
        // Mark this Item as having been picked up
        item.WasPickedUp = true;
        // Notify subscribers that we picked up a new item
        ItemPickedUp?.Invoke(item);
    }

    public void Equip(Item item)
    {
        // We don't want multiple items equipped at the same time
        UnequipActiveItem();
        // Place this item underneath our right hand
        item.transform.SetParent(_rightHand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.transform.localScale = Vector3.one;
        ActiveItem = item;
        // Invoke the Event if the Event is not null
        ActiveItemChanged?.Invoke(ActiveItem);
    }

    // Unequip the currently Active item, if one exists
    private void UnequipActiveItem()
    {
        if (ActiveItem != null)
        {
            ActiveItem.transform.SetParent(_itemRoot);
            ActiveItem.gameObject.SetActive(false);
        }
    }

}