using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    private UIInventorySlot[] _slots;
    public int SlotCount => _slots.Length;

    private void Awake()
    {
        _slots = GetComponentsInChildren<UIInventorySlot>();
    }
}