using UnityEngine;

public class LootItemHolder : MonoBehaviour
{
    [SerializeField] private Transform _itemTransform;
    [SerializeField] private float _rotationSpeed = 1f;
    
    private Item _item;

    private void Update()
    {
        float rotationAmount = Time.deltaTime * _rotationSpeed;
        _itemTransform.Rotate(0, rotationAmount, 0);
    }
    
    public void SetItem(Item item)
    {
        _item = item;
        _item.transform.SetParent(_itemTransform);
        _item.transform.localPosition = Vector3.zero;
        _item.transform.localRotation = Quaternion.identity;
        _item.gameObject.SetActive(true);
        _item.WasPickedUp = false;
        var collider = _item.GetComponent<SphereCollider>();
        if (collider != null)
        {
            collider.enabled = true;
        }
        _item.OnPickedUp += HandleItemPickedUp;
    }

    private void HandleItemPickedUp()
    {
        _item.OnPickedUp -= HandleItemPickedUp;
        LootSystem.AddToPool(this);
    }
}