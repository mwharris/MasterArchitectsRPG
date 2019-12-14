using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    
    public Item Item { get; private set; }
    public bool IsEmpty => Item == null;

    public void SetItem(Item item)
    {
        Item = item;
        _icon.sprite = item.Icon;
    }
}