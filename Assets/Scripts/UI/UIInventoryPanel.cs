using System;
using System.Linq;
using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    public event Action OnSelectionChanged;
    
    public UIInventorySlot[] Slots;
    public int SlotCount => Slots.Length;
    public UIInventorySlot Selected { get; private set; }

    private Inventory _inventory;
    
    private void Awake()
    {
        // Find all UIInventorySlot in the scene (includes inventory panel and hotbar)
        Slots = FindObjectsOfType<UIInventorySlot>()
            .OrderByDescending(t => t.SortIndex)
            .ThenBy(t => t.name)
            .ToArray();
        RegisterSlotClicks();
    }

    private void RegisterSlotClicks()
    {
        foreach (var slot in Slots)
        {
            slot.OnSlotClicked += HandleSlotClicked;
        }
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

    private void HandleSlotClicked(UIInventorySlot slot)
    {
        // Swap slots if multiple were selected
        if (Selected != null)
        {
            Swap(slot);
            Selected.BecomeUnselected();
            Selected = null;
        }
        // Don't select empty slots
        else if (!slot.IsEmpty)
        {
            Selected = slot;
            Selected.BecomeSelected();
        }
        OnSelectionChanged?.Invoke();
    }

    private void Swap(UIInventorySlot slot)
    {
        var selectedSlotIndex = GetSlotIndex(Selected);
        var destSlotIndex = GetSlotIndex(slot);
        if (!(selectedSlotIndex == -1 || destSlotIndex == -1))
        {
            _inventory.Move(selectedSlotIndex, destSlotIndex);
        }
    }

    private int GetSlotIndex(UIInventorySlot slotToFind)
    {
        for (int i = 0; i < SlotCount; i++)
        {
            if (Slots[i] == slotToFind)
            {
                return i;
            }
        }
        return -1;
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