using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image _crosshairImage;
    [SerializeField] private Sprite _gunSprite;
    [SerializeField] private Sprite _invalidSprite;

    
    private Inventory _inventory;

    private void OnEnable()
    {
        _inventory = FindObjectOfType<Inventory>();
        _inventory.ActiveItemChanged += HandleActiveItemChanged;

        _crosshairImage.sprite = _invalidSprite;
        if (_inventory.ActiveItem != null)
        {
            HandleActiveItemChanged(_inventory.ActiveItem);
        }
    }

    private void HandleActiveItemChanged(Item item)
    {
        Debug.Log($"Crosshair detected: {item.CrosshairMode}");
        switch (item.CrosshairMode)
        {
            case CrosshairMode.Gun:
                _crosshairImage.sprite = _gunSprite;
                break;
            case CrosshairMode.Invalid:
                _crosshairImage.sprite = _invalidSprite;
                break;
        }
    }
}

public enum CrosshairMode
{
    Invalid,
    Gun
}
