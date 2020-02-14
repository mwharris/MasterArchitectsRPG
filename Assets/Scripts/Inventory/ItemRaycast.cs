using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRaycast : ItemComponent
{
    [SerializeField] private float _delay = 0.1f;
    [SerializeField] private float _range = 10f;
    [SerializeField] private int _damage = 1;
    
    private RaycastHit[] _results = new RaycastHit[100];
    private int _layerMask;

    void Awake()
    {
        _layerMask = LayerMask.GetMask("Default");
    }

    public override void Use()
    {
        // Tell ItemComponent to increase our next use time
        _nextUseTime = Time.time + _delay;
        
        // Get a ray extending out of the center point of the screen
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2);

        // Raycast along the Default layer mask
        int hits = Physics.RaycastNonAlloc(ray, _results, _range, _layerMask, QueryTriggerInteraction.Collide);

        // Find the closest object we hit
        RaycastHit nearest = new RaycastHit();
        double nearestDistance = double.MaxValue;
        for (int i = 0; i < hits; i++)
        {
            RaycastHit currHit = _results[i];
            var distance = Vector3.Distance(transform.position, currHit.point);
            if (distance < nearestDistance && !currHit.transform.IsChildOf(this.transform))
            {
                nearestDistance = distance;
                nearest = currHit;
            }
        }

        // Tell whatever we hit to take damage
        if (nearest.transform != null)
        {
            var takeHits = nearest.collider.GetComponent<ITakeHits>();
            takeHits?.TakeHit(_damage);
        }
    }
}