using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<Item> ActiveItemChanged;
    
    [SerializeField] private Transform _rightHand;
    
    private List<Item> _items = new List<Item>();
    private Transform _itemRoot;
    
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
    }

    private void Equip(Item item)
    {
        // Place this item underneath our right hand
        item.transform.SetParent(_rightHand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        ActiveItem = item;

        // Invoke the Event if the Event is not null
        ActiveItemChanged?.Invoke(ActiveItem);
    }

}