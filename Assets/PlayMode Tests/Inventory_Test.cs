using NUnit.Framework;
using UnityEngine;

namespace PlayMode_Tests
{
    public class Inventory_Test
    {
        // Add Items
        [Test]
        public void Can_Add_Items()
        {
            Inventory inventory = new GameObject("INVENTORY").AddComponent<Inventory>();
            Item item = new GameObject("ITEM", typeof(SphereCollider)).AddComponent<Item>();
            
            Assert.AreEqual(0, inventory.Count);
            inventory.Pickup(item);
            Assert.AreEqual(1, inventory.Count);
        }
        
        // Place into a specific slot
        [Test]
        public void Can_Add_Item_To_Specific_Slot()
        {
            Inventory inventory = new GameObject("INVENTORY").AddComponent<Inventory>();
            Item item = new GameObject("ITEM", typeof(SphereCollider)).AddComponent<Item>();
            
            inventory.Pickup(item, 5);
            Assert.AreEqual(item, inventory.GetItemInSlot(5));
        }
        
        // Change slot / move
        [Test]
        public void Can_Move_Item_To_Empty_Slot()
        {
            Inventory inventory = new GameObject("INVENTORY").AddComponent<Inventory>();
            Item item = new GameObject("ITEM", typeof(SphereCollider)).AddComponent<Item>();
             
            inventory.Pickup(item, 0);
            Assert.AreEqual(item, inventory.GetItemInSlot(0));

            inventory.Move(0, 4);
            Assert.AreEqual(item, inventory.GetItemInSlot(4));
        }
        
        // Remove Items
        // Drop Items
        // Hotkey/Hotbar assignment
        // Change visuals?
        // Modify stats
        // Persist & load
    }
}