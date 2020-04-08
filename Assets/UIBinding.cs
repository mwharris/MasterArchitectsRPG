using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBinding : MonoBehaviour
{
    IEnumerator Start()
    {
        var inventoryPanel = GetComponent<UIInventoryPanel>();
        var player = FindObjectOfType<Player>();
        
        // This is needed because we are asynchronously loading our scenes.
        // So it's possible this script (in the UI scene) is loaded before the player scene.
        // This will wait until both scenes are done loading to get the player.
        while (player == null)
        {
            yield return null;
            player = FindObjectOfType<Player>();
        }

        inventoryPanel.Bind(player.GetComponent<Inventory>());
    }

}
