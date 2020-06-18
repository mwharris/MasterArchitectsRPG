using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IPointerDownHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<UIInventorySlot> OnSlotClicked;

    [SerializeField] private Image _image;
    [SerializeField] private int _sortIndex;
    [SerializeField] private Image selectedImage;
    [SerializeField] private Image focusedImage;

    public UIInventoryPanel _inventoryPanel;
    public IItem Item { get; private set; }
    public bool IsEmpty => Item == null;
    public Sprite Icon => _image.sprite;
    public bool IconImageEnabled => _image.enabled;
    public int SortIndex => _sortIndex;

    private void Awake()
    {
        _inventoryPanel = FindObjectOfType<UIInventoryPanel>();
    }
    
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (focusedImage && _inventoryPanel.Selected != null)
        {
            focusedImage.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData) => DisableFocusImage();
    public void OnDisable() => DisableFocusImage();
    private void DisableFocusImage()
    {
        if (focusedImage)
        {
            focusedImage.enabled = false;
        }
    }

    public void BecomeSelected()
    {
        if (selectedImage)
        {
            selectedImage.enabled = true;
        }
    }

    public void BecomeUnselected()
    {
        if (selectedImage)
        {
            selectedImage.enabled = false;
        }
    }
}