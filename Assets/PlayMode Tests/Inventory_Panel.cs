using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayMode_Tests
{
    public class Inventory_Panel
    {
        public static UIInventoryPanel GetInventoryPanel()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UIInventoryPanel>("Assets/Prefabs/UI/InventoryPanel.prefab");
            return Object.Instantiate(prefab);
        }
        
        private Inventory GetInventory(int numberOfItems = 0)
        {
            return Inventory_Helpers.GetInventory(numberOfItems);
        }
        
        private Item GetItem()
        {
            return Inventory_Helpers.GetItem();
        }

        [UnityTearDown]
        public IEnumerator Teardown()
        {
            var inventory = Object.FindObjectOfType<Inventory>();
            var inventoryPanel = Object.FindObjectOfType<UIInventoryPanel>();
            
            if (inventory)
            {
                Object.Destroy(inventory.gameObject);
            }
            if (inventoryPanel)
            {
                Object.Destroy(inventoryPanel.gameObject);
            }

            yield return null;
        }
        
        [Test]
        public void has_25_slots()
        {
            var inventoryPanel = GetInventoryPanel();
            Assert.AreEqual(25, inventoryPanel.SlotCount);
        }

        [Test]
        public void bound_to_empty_inventory_has_all_slots_empty()
        {
            var inventoryPanel = GetInventoryPanel();
            var inventory = GetInventory();

            inventoryPanel.Bind(inventory);

            foreach (var slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }
        }

        [Test]
        public void bound_to_null_inventory_has_all_slots_empty()
        {
            var inventoryPanel = GetInventoryPanel();

            inventoryPanel.Bind(null);
            
            foreach (var slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }
        }

        [Test]
        public void bound_to_valid_then_null_inventory_has_all_slots_empty()
        {
            var inventoryPanel = GetInventoryPanel();
            var inventory = GetInventory();
            var item = GetItem();
            
            inventory.Pickup(item);
            inventoryPanel.Bind(inventory);
            Assert.IsFalse(inventoryPanel.Slots[0].IsEmpty);
            
            inventoryPanel.Bind(null);
            Assert.IsTrue(inventoryPanel.Slots[0].IsEmpty);
        }

        [UnityTest]
        public IEnumerator bound_to_inventory_fills_slot_for_each_item([NUnit.Framework.Range(0, 25)] int numberOfItems)
        {
            var inventoryPanel = GetInventoryPanel();
            var inventory = GetInventory(numberOfItems);

            foreach (var slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }
            
            inventoryPanel.Bind(inventory);
            for (int i = 0; i < inventoryPanel.SlotCount; i++)
            {
                bool shouldBeEmpty = i >= numberOfItems;
                Assert.AreEqual(shouldBeEmpty, inventoryPanel.Slots[i].IsEmpty);
            }
            
            // UnityTeardown doesn't work for this test because it's a looped test.
            // The Teardown won't run until all 25 versions have completed.
            // Hence the need for the following code:
            Object.Destroy(inventoryPanel.gameObject);
            Object.Destroy(inventory.gameObject);
            yield return null;
        }

        [Test]
        public void updates_when_item_picked_up()
        {
            var inventoryPanel = GetInventoryPanel();
            var inventory = GetInventory();
            var item = GetItem();
            
            inventoryPanel.Bind(inventory);
            Assert.IsTrue(inventoryPanel.Slots[0].IsEmpty);
            
            inventory.Pickup(item);
            Assert.IsFalse(inventoryPanel.Slots[0].IsEmpty);
        }
        
        [Test]
        public void updates_slots_when_items_are_moved()
        {
            var inventoryPanel = GetInventoryPanel();
            var inventory = GetInventory(1);
            
            inventoryPanel.Bind(inventory);
            
            inventory.Move(0,4);
            
            Assert.AreSame(inventory.Items[4], inventoryPanel.Slots[4].Item);
        }
    }
}