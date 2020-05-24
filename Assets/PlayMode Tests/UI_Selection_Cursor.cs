using System.Collections;
using A_Player;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
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
            
            inventoryPanel.Slots[0].OnPointerDown(null);
            
            Assert.IsTrue(uiCursor.IconVisible);
        }

        [Test]
        public void icon_sprite_shows_when_item_selected()
        {
            var inventoryPanel = Inventory_Helpers.GetInventoryPanelWithItems(1);
            var uiCursor = Inventory_Helpers.GetSelectionCursor();
            
            inventoryPanel.Slots[0].OnPointerDown(null);
            
            Assert.AreSame(inventoryPanel.Slots[0].Icon, uiCursor.Icon);
        }

        [Test]
        public void icon_not_visible_when_item_unselected()
        {
            var inventoryPanel = Inventory_Helpers.GetInventoryPanelWithItems(2);
            var uiCursor = Inventory_Helpers.GetSelectionCursor();
            
            Assert.IsFalse(uiCursor.IconVisible);
            inventoryPanel.Slots[0].OnPointerDown(null);
            Assert.IsTrue(uiCursor.IconVisible);
            inventoryPanel.Slots[1].OnPointerDown(null);
            Assert.IsFalse(uiCursor.IconVisible);
        }

        [UnityTest]
        public IEnumerator moves_with_mouse_cursor()
        {
            yield return Helpers.LoadItemTestScene();
            var uiCursor = Object.FindObjectOfType<UISelectionCursor>();
            PlayerInput.Instance = Substitute.For<IPlayerInput>();

            for (int i = 0; i < 100; i++)
            {
                var mousePosition = new Vector2(i, i);
                PlayerInput.Instance.MousePosition.Returns(mousePosition);

                yield return null;

                Vector3 mousePosition3 = new Vector3(mousePosition.x, mousePosition.y, 0);
                Assert.AreEqual(mousePosition3, uiCursor.transform.position);
            }
        }

        [Test]
        public void raycast_target_disabled()
        {
            var inventoryPanel = Inventory_Helpers.GetInventoryPanelWithItems();
            var uiCursor = Inventory_Helpers.GetSelectionCursor();
            var image = uiCursor.GetComponent<Image>();
            Assert.IsFalse(image.raycastTarget);
        }
    }
}