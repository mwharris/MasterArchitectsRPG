using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class HotbarSlot : MonoBehaviour
{
    [FormerlySerializedAs("_icon")] [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _text;
    
    private UIInventorySlot _inventorySlot;

    public IItem Item => _inventorySlot.Item;
    public bool IsEmpty => Item == null;
    public Image IconImage => _iconImage;

    private void Awake() => _inventorySlot = GetComponent<UIInventorySlot>();

    private void OnValidate()
    {
        _text = GetComponentInChildren<TMP_Text>();

        int index = transform.GetSiblingIndex() + 1;
        _text.SetText(index.ToString());

        gameObject.name = "Slot " + index;
    }
}