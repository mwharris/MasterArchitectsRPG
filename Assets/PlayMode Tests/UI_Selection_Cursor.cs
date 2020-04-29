using NUnit.Framework;

namespace PlayMode_Tests
{
    public class UI_Selection_Cursor
    {
        [Test]
        public void no_icon_when_no_item_selected()
        {
            var uiCursor = Inventory_Helpers.GetSelectionCursor();
            Assert.IsFalse(uiCursor.IconVisible);
        }

        [Test]
        public void icon_shows_when_item_selected()
        {
            var inventoryPanel = Inventory_Helpers.GetInventoryPanelWithItems(1);
            var uiCursor = Inventory_Helpers.GetSelectionCursor();
            
            inventoryPanel.Slots[0].OnPointerClick(null);
            
            Assert.IsTrue(uiCursor.IconVisible);
        }
    }
}