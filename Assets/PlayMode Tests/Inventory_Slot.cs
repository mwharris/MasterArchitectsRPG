using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace PlayMode_Tests
{
    public class Inventory_Slot
    {
        [Test]
        public void when_item_set_shows_item_icon()
        {
            var inventoryPanel = Inventory_Panel.GetInventoryPanel();
            var slot = inventoryPanel.Slots[0];

            var item = Substitute.For<IItem>();
            var sprite = Sprite.Create(Texture2D.redTexture, new Rect(0, 0, 4, 4), Vector2.zero);
            item.Icon.Returns(sprite);
            
            slot.SetItem(item);
            Assert.AreSame(sprite, slot.Icon);
        }

        [Test]
        public void when_item_set_image_is_enabled()
        {
            var inventoryPanel = Inventory_Panel.GetInventoryPanel();
            var slot = inventoryPanel.Slots[0];

            var item = Substitute.For<IItem>();
            var sprite = Sprite.Create(Texture2D.redTexture, new Rect(0, 0, 4, 4), Vector2.zero);
            item.Icon.Returns(sprite);

            slot.SetItem(item);
            Assert.IsTrue(slot.IconImageEnabled);
        }

        [Test]
        public void when_item_not_set_image_is_disabled()
        {
            var inventoryPanel = Inventory_Panel.GetInventoryPanel();
            var slot = inventoryPanel.Slots[0];
            Assert.IsFalse(slot.IconImageEnabled);
        }
    }
}