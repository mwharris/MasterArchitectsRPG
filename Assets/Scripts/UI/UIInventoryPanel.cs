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
        }
        
        _inventory = inventory;
        _inventory.ItemPickedUp += HandleItemPickedUp;
        RefreshSlots();
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
}