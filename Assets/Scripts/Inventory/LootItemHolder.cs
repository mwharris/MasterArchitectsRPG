using UnityEngine;

public class LootItemHolder : MonoBehaviour
{
    [SerializeField] private Transform _itemTransform;
    [SerializeField] private float _rotationSpeed = 1f;

    private void Update()
    {
        float rotationAmount = Time.deltaTime * _rotationSpeed;
        _itemTransform.Rotate(0, rotationAmount, 0);
    }
    
    public void SetItem(Item item)
    {
        item.transform.SetParent(_itemTransform);
        item.transform.localPosition = Vector3.zero;
        item.gameObject.SetActive(true);
    }
}
