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
        [UnityTest]
        public IEnumerator Picks_Up_And_Equips_Item()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            Item item = GameObject.FindObjectOfType<Item>();
            
            Assert.AreNotSame(item, player.GetComponent<Inventory>().ActiveItem);
            
            player.PlayerInput.Vertical.Returns(1f);
            yield return new WaitForSeconds(1f);

            Assert.NotNull(player.GetComponent<Inventory>().ActiveItem);
            Assert.AreSame(item, player.GetComponent<Inventory>().ActiveItem);
        }
        
        [UnityTest]
        public IEnumerator Changes_Crosshair_To_Item_Crosshair()
        {
            yield return Helpers.LoadItemTestScene();

            var player = Helpers.GetPlayer();
            var crosshair = GameObject.FindObjectOfType<Crosshair>();
            Item item = GameObject.FindObjectOfType<Item>();
            
            Assert.AreNotSame(item.CrosshairDefinition.Sprite, crosshair.GetComponent<Image>().sprite);

            item.transform.position = player.transform.position;
            yield return null;

            Assert.NotNull(player.GetComponent<Inventory>().ActiveItem);
            Assert.AreEqual(item.CrosshairDefinition.Sprite, crosshair.GetComponent<Image>().sprite);
        }
    }
}