using UnityEngine;

public class UISelectionCursor : MonoBehaviour
{
    public bool IconVisible { get; set; } = false;
    
    private UIInventoryPanel _inventoryPanel;

    private void Awake() =>_inventoryPanel = FindObjectOfType<UIInventoryPanel>();

    private void OnEnable() => _inventoryPanel.OnSelectionChanged += HandleSelectionChanged;
    private void OnDisable() => _inventoryPanel.OnSelectionChanged -= HandleSelectionChanged;

    private void HandleSelectionChanged() => IconVisible = !_inventoryPanel.Selected.IsEmpty;
    
}