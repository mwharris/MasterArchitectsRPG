using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using UnityEngine;

public class Stats_Test : MonoBehaviour
{
    [Test]
    public void can_add()
    {
        Stats stats = new Stats();
        // Create a new MoveSpeed stat with value 3
        stats.Add(StatType.MoveSpeed, 3f);
        Assert.AreEqual(3f, stats.Get(StatType.MoveSpeed));
        // Increment the now existing MoveSpeed stat by 5
        stats.Add(StatType.MoveSpeed, 5f);
        Assert.AreEqual(8f, stats.Get(StatType.MoveSpeed));
    }

    [Test]
    public void can_remove()
    {
        Stats stats = new Stats();
        // Create a new MoveSpeed stat with value 3
        stats.Add(StatType.MoveSpeed, 3f);
        Assert.AreEqual(3f, stats.Get(StatType.MoveSpeed));
        // Remove the MoveSpeed stat
        stats.Remove(StatType.MoveSpeed, stats.Get(StatType.MoveSpeed));
        Assert.AreEqual(0f, stats.Get(StatType.MoveSpeed));
    }
}