using NUnit.Framework;
using UnityEngine.UI;

namespace PlayMode_Tests
{
    public class UI_Selection_Cursor
    {
        [Test]
        public void in_default_state_shows_no_icon()
        {
            var inventoryPanel = Inventory_Helpers.GetInventoryPanelWithItems(1);
            var uiCursor = Inventory_Helpers.GetSelectionCursor();
            Assert.IsFalse(uiCursor.IconVisible);
            Assert.IsFalse(uiCursor.GetComponent<Image>().enabled);
        }

        [Test]
        public void icon_shows_when_item_selected()
        {
            var inventoryPanel = Inventory_Helpers.GetInventoryPanelWithItems(1);
            var uiCursor = Inventory_Helpers.GetSelectionCursor();
            
            inventoryPanel.Slots[0].OnPointerClick(null);
            
            Assert.IsTrue(uiCursor.IconVisible);
        }

        [Test]
        public void icon_sprite_shows_when_item_selected()
        {
            var inventoryPanel = Inventory_Helpers.GetInventoryPanelWithItems(1);
            var uiCursor = Inventory_Helpers.GetSelectionCursor();
            
            inventoryPanel.Slots[0].OnPointerClick(null);
            
            Assert.AreSame(inventoryPanel.Slots[0].Icon, uiCursor.Icon);
        }
        
        [Test]
        public void icon_not_visible_when_item_unselected()
        {
            var inventoryPanel = Inventory_Helpers.GetInventoryPanelWithItems(2);
            var uiCursor = Inventory_Helpers.GetSelectionCursor();
            
            Assert.IsFalse(uiCursor.IconVisible);
            inventoryPanel.Slots[0].OnPointerClick(null);
            Assert.IsTrue(uiCursor.IconVisible);
            inventoryPanel.Slots[1].OnPointerClick(null);
            Assert.IsFalse(uiCursor.IconVisible);
        }
    }
}