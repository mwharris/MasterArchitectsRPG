using UnityEngine;
using UnityEngine.UI;

public class UISelectionCursor : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    private UIInventoryPanel _inventoryPanel;
    
    public bool IconVisible => _image != null && _image.sprite != null;
    public Sprite Icon => _image.sprite;

    private void Awake() =>_inventoryPanel = FindObjectOfType<UIInventoryPanel>();

    private void OnEnable() => _inventoryPanel.OnSelectionChanged += HandleSelectionChanged;
    private void OnDisable() => _inventoryPanel.OnSelectionChanged -= HandleSelectionChanged;

    private void HandleSelectionChanged()
    {
        _image.sprite = _inventoryPanel.Selected?.Icon;
    }
}