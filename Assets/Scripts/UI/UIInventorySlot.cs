using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    [SerializeField] private Image _image;    // Unity UI Image class

    public IItem Item { get; private set; }
    public bool IsEmpty => Item == null;
    public Sprite Icon => _image.sprite;

    public void SetItem(IItem item)
    {
        Item = item;
        _image.sprite = item != null ? item.Icon : null;
    }

    public void ClearItem()
    {
        Item = null;
    }
}