﻿using System.Collections.Generic;

public class Stats
{
    private Dictionary<StatType, float> _stats = new Dictionary<StatType, float>();

    public void Bind(Inventory inventory)
    {
        // Stats on items only apply when an item is equipped so subscribe to equip events
        inventory.ItemEquipped += HandleItemEquipped;
        inventory.ItemUnEquipped += HandleItemUnEquipped;
    }

    private void HandleItemEquipped(Item item)
    {
        foreach (var statMod in item.StatMods)
        {
            Add(statMod.StatType, statMod.Value);
        }
    }

    private void HandleItemUnEquipped(Item item)
    {
        foreach (var statMod in item.StatMods)
        {
            Remove(statMod.StatType, statMod.Value);
        }
    }

    public void Add(StatType statType, float value)
    {
        if (_stats.ContainsKey(statType))
        {
            _stats[statType] += value;
        }
        else
        {
            _stats[statType] = value;
        }
    }

    public float Get(StatType statType)
    {
        return _stats[statType];
    }

    public void Remove(StatType statType, float value)
    {
        if (_stats.ContainsKey(statType))
        {
            _stats[statType] -= value;
        }
    }
}