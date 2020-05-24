using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IPointerDownHandler, IEndDragHandler, IDragHandler
{
    public event Action<UIInventorySlot> OnSlotClicked;
    
    [SerializeField] private Image _image;

    public IItem Item { get; private set; }
    public bool IsEmpty => Item == null;
    public Sprite Icon => _image.sprite;
    public bool IconImageEnabled => _image.enabled;

    public void SetItem(IItem item)
    {
        Item = item;
        _image.sprite = item != null ? item.Icon : null;
        _image.enabled = item != null;
    }

    public void ClearItem()
    {
        Item = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSlotClicked?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData) { }
    public void OnEndDrag(PointerEventData eventData)
    {
        // Get the UIInventorySlot of the slot we dropped the item on
        var droppedOnSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<UIInventorySlot>();
        // Simulate a click on the element we dropped on
        if (droppedOnSlot != null)
        {
            droppedOnSlot.OnPointerDown(eventData);
        }
        else
        {
            OnPointerDown(eventData);
        }
    }

}