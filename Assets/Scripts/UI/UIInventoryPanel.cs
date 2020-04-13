using System.Collections;
using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    public UIInventorySlot[] Slots;
    public int SlotCount => Slots.Length;

    private Inventory _inventory;
    
    private void Awake()
    {
        Slots = GetComponentsInChildren<UIInventorySlot>();
    }

    public void Bind(Inventory inventory)
    {
        if (_inventory != null)
        {
            _inventory.ItemPickedUp -= HandleItemPickedUp;
            _inventory.OnItemChanged -= HandleItemChanged;
        }

        _inventory = inventory;
        
        if (_inventory != null)
        {
            _inventory.ItemPickedUp += HandleItemPickedUp;
            _inventory.OnItemChanged += HandleItemChanged;
            RefreshSlots();
        }
        else
        {
            ClearSlots();
        }
    }

    private void HandleItemChanged(int slotIndex)
    {
        var slot = Slots[slotIndex];
        var item = _inventory.GetItemInSlot(slotIndex);
        slot.SetItem(item);
    }


    private void HandleItemPickedUp(Item item)
    {
        RefreshSlots();
    }
    
    private void RefreshSlots()
    {
        for (var i = 0; i < Slots.Length; i++)
        {
            var slot = Slots[i];
            if (_inventory.Items.Count > i)
            {
                slot.SetItem(_inventory.Items[i]);
            }
            else
            {
                slot.ClearItem();
            }
        }
    }
    
    private void ClearSlots()
    {
        foreach (var slot in Slots)
        {
            slot.ClearItem();
        }
    }
}