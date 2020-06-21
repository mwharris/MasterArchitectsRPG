using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int DEFAULT_INVENTORY_SIZE = 29;
    
    public event Action<IItem> ActiveItemChanged;
    public event Action<Item> ItemPickedUp;
    public event Action<int> OnItemChanged;
    public event Action<IItem> ItemEquipped;
    public event Action<IItem> ItemUnEquipped;

    [SerializeField] private Transform _rightHand;
    private Transform _itemRoot;
    
    private Item[] _items = new Item[DEFAULT_INVENTORY_SIZE];

    public IItem ActiveItem { get; private set; }
    
    public List<Item> Items => _items.ToList();    // TODO: Performance issue here
    public int Count => _items.Count(t => t != null);

    void Awake()
    {
        // Create our item root as a child of this game object
        _itemRoot = new GameObject("Items").transform;
        _itemRoot.transform.SetParent(transform);
    }

    public void Pickup(Item item, int? slot = null)
    {
        if (slot.HasValue == false)
        {
            slot = FindFirstAvailableSlot();
            if (slot.HasValue == false)
            {
                return;
            }
        }
        // Add the item to our list of items
        _items[slot.Value] = item;
        // Child it to this game object
        item.transform.parent = _itemRoot;
        // Equip this item
        Equip(item);
        // Mark this Item as having been picked up
        item.WasPickedUp = true;
        // Notify subscribers that we picked up a new item
        ItemPickedUp?.Invoke(item);
    }

    private int? FindFirstAvailableSlot()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
            {
                return i;
            }
        }
        return null;
    }

    public void Equip(IItem item)
    {
        // We don't want multiple items equipped at the same time
        UnequipActiveItem();
        // Place this item underneath our right hand
        HandleItemEquipped(item);
        // Invoke the Event if the Event is not null
        ActiveItemChanged?.Invoke(ActiveItem);
        ItemEquipped?.Invoke(item);
    }

    // Unequip the currently Active item, if one exists
    private void UnequipActiveItem()
    {
        if (ActiveItem != null)
        {
            ActiveItem.transform.SetParent(_itemRoot);
            ActiveItem.gameObject.SetActive(false);
            ItemUnEquipped?.Invoke(ActiveItem);
        }
    }
    
    private void HandleItemEquipped(IItem item)
    {
        item.transform.SetParent(_rightHand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.transform.localScale = Vector3.one;
        ActiveItem = item;
        ActiveItem.gameObject.SetActive(true);
    }

    public Item GetItemInSlot(int slotIndex)
    {
        return _items[slotIndex];
    }

    public void Move(int sourceIndex, int destIndex)
    {
        var destItem = _items[destIndex];
        _items[destIndex] = _items[sourceIndex];
        _items[sourceIndex] = destItem;

        OnItemChanged?.Invoke(destIndex);
        OnItemChanged?.Invoke(sourceIndex);
    }
}