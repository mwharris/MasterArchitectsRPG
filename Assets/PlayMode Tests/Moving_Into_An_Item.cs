using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace A_Player
{
    public class Moving_Into_An_Item
    {
        private Player player;
        private Item item;

        [UnitySetUp]
        public IEnumerator Init()
        {
            yield return Helpers.LoadItemTestScene();

            item = GameObject.FindObjectOfType<Item>();

            player = Helpers.GetPlayer();
            player.PlayerInput.Vertical.Returns(1f);
        }
        
        [UnityTest]
        public IEnumerator Picks_Up_And_Equips_Item()
        {
            Assert.AreNotSame(item, player.GetComponent<Inventory>().ActiveItem);
            
            yield return new WaitForSeconds(1f);

            Assert.NotNull(player.GetComponent<Inventory>().ActiveItem);
            Assert.AreSame(item, player.GetComponent<Inventory>().ActiveItem);
        }
        
        [UnityTest]
        public IEnumerator Changes_Crosshair_To_Item_Crosshair()
        {
            var crosshair = GameObject.FindObjectOfType<Crosshair>();
            Assert.AreNotSame(item.CrosshairDefinition.Sprite, crosshair.GetComponent<Image>().sprite);

            item.transform.position = player.transform.position;
            yield return null;

            Assert.NotNull(player.GetComponent<Inventory>().ActiveItem);
            Assert.AreEqual(item.CrosshairDefinition.Sprite, crosshair.GetComponent<Image>().sprite);
        }
        
        [UnityTest]
        public IEnumerator Changes_Slot1_Icon_To_Match_Item_Icon()
        {
            var hotbar = GameObject.FindObjectOfType<Hotbar>();
            var slot1 = hotbar.GetComponentInChildren<Slot>();
            
            Assert.AreNotSame(item.Icon, slot1.IconImage.sprite);

            item.transform.position = player.transform.position;
            yield return new WaitForFixedUpdate();

            Assert.NotNull(player.GetComponent<Inventory>().ActiveItem);
            Assert.AreEqual(item.Icon, slot1.IconImage.sprite);
        }
    }
}