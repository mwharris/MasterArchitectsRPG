using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayMode_Tests
{
    public class Inventory_Panel
    {
        private UIInventoryPanel GetInventoryPanel()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UIInventoryPanel>("Assets/Prefabs/UI/InventoryPanel.prefab");
            return Object.Instantiate(prefab);
        }
        
        private Inventory GetInventory()
        {
            return new GameObject("Inventory").AddComponent<Inventory>();
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
        public void bound_to_inventory_with_1_item_fills_first_slot()
        {
            var inventoryPanel = GetInventoryPanel();
            var inventory = GetInventory();
            var item = GetItem();

            Assert.IsTrue(inventoryPanel.Slots[0].IsEmpty);
            
            inventory.Pickup(item);
            inventoryPanel.Bind(inventory);
            
            Assert.IsFalse(inventoryPanel.Slots[0].IsEmpty);
        }

        private Item GetItem()
        {
            var go = new GameObject("Item", typeof(SphereCollider));
            return go.AddComponent<Item>();
        }

    }
}