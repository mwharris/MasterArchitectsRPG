using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image _crosshairImage;
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

    private void HandleActiveItemChanged(IItem item)
    {
        if (item != null && item.CrosshairDefinition != null)
        {
            _crosshairImage.sprite = item.CrosshairDefinition.Sprite;
        }
        else
        {
            _crosshairImage.sprite = _invalidSprite;
        }
    }
}