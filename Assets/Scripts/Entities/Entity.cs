using System;
using UnityEngine;

public class Entity : MonoBehaviour, ITakeHits
{
    public Action OnDied;
    
    [SerializeField] private int _maxHealth = 5;
    public int Health { get; set; }

    private void OnEnable()
    {
        Health = _maxHealth;
    }

    public void TakeHit(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
        else
        {
            HandleNonLethalHit();
        }
    }

    private void HandleNonLethalHit()
    {
        Debug.Log("Took Non-Lethal Damage");
    }

    private void Die()
    {
        OnDied?.Invoke();
        Debug.Log("WE DED!");
    }
}