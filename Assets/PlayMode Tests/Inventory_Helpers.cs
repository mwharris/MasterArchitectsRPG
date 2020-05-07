using UnityEditor;
using UnityEngine;

namespace PlayMode_Tests
{
    public static class Inventory_Helpers
    {
        public static UIInventoryPanel GetInventoryPanelWithItems(int numberOfItems = 0)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UIInventoryPanel>("Assets/Prefabs/UI/InventoryPanel.prefab");
            var inventoryPanel = Object.Instantiate(prefab);
            var inventory = GetInventory(numberOfItems);
            inventoryPanel.Bind(inventory);
            return inventoryPanel;
        }
        
        public static Inventory GetInventory(int numberOfItems = 0)
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            for (int i = 0; i < numberOfItems; i++)
            {
                var item = GetItem();
                inventory.Pickup(item);
            }
            return inventory;
        }
        
        public static Item GetItem()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Item>("Assets/Prefabs/Items/TestItem.prefab");
            return Object.Instantiate(prefab);
        }

        public static UISelectionCursor GetSelectionCursor()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UISelectionCursor>("Assets/Prefabs/UI/SelectionCursor.prefab");
            return Object.Instantiate(prefab);
        }
    }
}