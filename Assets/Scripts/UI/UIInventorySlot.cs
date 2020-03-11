using UnityEngine;

public class UIInventorySlot : MonoBehaviour
{
    private Item _item;
    public bool IsEmpty => _item == null;

    public void SetItem(Item item)
    {
        _item = item;
    }

    public void ClearItem()
    {
        _item = null;
    }
}