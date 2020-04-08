using System.Collections;
using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    public UIInventorySlot[] Slots;
    public int SlotCount => Slots.Length;

    private void Awake()
    {
        Slots = GetComponentsInChildren<UIInventorySlot>();
    }

    public void Bind(Inventory inventory)
    {
        //inventory.ItemPickedUp += 
        
        for (var i = 0; i < Slots.Length; i++)
        {
            var slot = Slots[i];
            if (inventory.Items.Count > i )
            {
                slot.SetItem(inventory.Items[i]);
            }
            else
            {
                slot.ClearItem();
            }
        }
    }
}