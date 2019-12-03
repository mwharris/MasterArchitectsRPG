using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRaycast : ItemComponent
{
    [SerializeField] private float _delay = 0.1f;
    [SerializeField] private float _range = 10f;
    
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
            var distance = Vector3.Distance(transform.position, _results[i].point);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = _results[i];
            }            
        }

        // Spawn a cube at the nearest hit location
        if (nearest.transform != null)
        {
            Transform hitCube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            hitCube.GetComponent<Renderer>().material.color = Color.red;
            hitCube.transform.localScale = (Vector3.one / 10);
            hitCube.transform.position = nearest.point;
        }
    }
}