using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class NPCLoot : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private Item[] _itemPrefabs;
    private EntityStateMachine _entityStateMachine;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _entityStateMachine = GetComponent<EntityStateMachine>();
    }

    private void Start()
    {
		// There's actually a small bug here.
		// During the Awake phase:
		// 	1. EntityStateMachine registers with StateMachine.OnStateChanged
		//	2. EntityStateMachine then calls StateMachine.SetState() which in turn invokes its OnStateChanged event.
		// 	3. This triggers EntityStateMachine.OnEntityStateChanged, which propogates the event
		// But this call here is in the Start phase, which happens after the Awake phase.
		// Therefore, it is not possible for Step 3 to ever trigger this event right here.
		// This is fine because the initial state is Idle and this is waiting for Dead, but it could cause problems.
        _entityStateMachine.OnEntityStateChanged += HandleEntityStateChanged;
        
        foreach (var itemPrefab in _itemPrefabs)
        {
            Item itemInstance = Instantiate(itemPrefab);
            _inventory.Pickup(itemInstance);
        }
    }

    private void HandleEntityStateChanged(IState state)
    {
        if (state is Dead)
        {
            DropLoot();
        }
    }

    private void DropLoot()
    {
        foreach (var item in _inventory.Items)
        {
            if (item != null)
            {
                LootSystem.Drop(item, transform);
            }
        }
        // We might not want to be able clear the inventory from outside like this...
        _inventory.Items.Clear();
    }
}