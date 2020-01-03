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
    }

    // Helper function to test when our player dies.
    // ContextMenu() puts an option in the right-click menu that will call this function when clicked.
    // TODO: Would this be better in ITakeHits?...
    [ContextMenu("Take Lethal Damage")]
    private void TakeLethalDamage()
    {
        TakeHit(Health);
    }
}