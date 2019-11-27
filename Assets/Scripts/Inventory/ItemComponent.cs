using System;
using UnityEngine;

public abstract class ItemComponent : MonoBehaviour
{
    private bool CanUse => Time.time >= _nextUseTime;
    
    protected float _nextUseTime;

    protected abstract void Use();
    
    private void Update()
    {
        if (CanUse && Input.GetKeyDown(KeyCode.Space))
        {
            Use();
            _nextUseTime = Time.time + 1f;
        }
    }
}