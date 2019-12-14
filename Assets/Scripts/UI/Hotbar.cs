using UnityEngine;

public class Hotbar : MonoBehaviour
{
    private Inventory _inventory;
    private Slot[] _slots;
    private Player _player;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _player.PlayerInput.HotkeyPressed += HotkeyPressed;
        
        _inventory = FindObjectOfType<Inventory>();
        _inventory.ItemPickedUp += ItemPickedUp;
        _slots = GetComponentsInChildren<Slot>();
    }

    private void HotkeyPressed(int index)
    {
        if(index >= _slots.Length || index < 0)
            return;
        
        if (!_slots[index].IsEmpty)
        {
            _inventory.Equip(_slots[index].Item);
        }
    }

    private void ItemPickedUp(Item item)
    {
        Slot slot = FindNextOpenSlot();
        if (slot != null)
        {
            slot.SetItem(item);
        }
    }

    private Slot FindNextOpenSlot()
    {
        foreach (Slot slot in _slots)
        {
            if (slot.IsEmpty)
            {
                return slot;     
            }
        }
        return null;
    }
}