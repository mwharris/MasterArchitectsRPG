using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour, IItem
{
    public event Action OnPickedUp;
    
    [SerializeField] private CrosshairDefinition _crosshairDefinition;
    [SerializeField] private UseAction[] _actions = new UseAction[0];
    [SerializeField] private Sprite _icon;
    [SerializeField] private StatMod[] _statMods;

    public CrosshairDefinition CrosshairDefinition => _crosshairDefinition;
    public UseAction[] Actions => _actions;
    public Sprite Icon => _icon;
    public StatMod[] StatMods => _statMods;
    public bool WasPickedUp { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (WasPickedUp)
        {
            return;
        }

        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.Pickup(this);
            OnPickedUp?.Invoke();
        }
    }

    // Make sure this GameObject has a Collider.
    // Automatically set isTrigger to True on that Collider.
    private void OnValidate()
    {
        var collider = GetComponent<Collider>();
        if (!collider.isTrigger)
        {
            collider.isTrigger = true;
        }
    }
}

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    // Runs when the inspector draws for the selected script
    // In this case the selected script is Item (see CustomEditor)
    public override void OnInspectorGUI()
    {
        Item item = (Item) target;

        DrawIcon(item);
        DrawCrosshair(item);
        DrawActions(item);
        DrawStatMods(item);
    }

    private void DrawIcon(Item item)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Icon", GUILayout.Width(120));
        if (item?.Icon != null)
        {
            GUILayout.Box(item.Icon.texture, GUILayout.Width(60), GUILayout.Height(60));
        }
        else
        {
            EditorGUILayout.HelpBox("No Icon Selected", MessageType.Warning);
        }

        using (SerializedProperty property = serializedObject.FindProperty("_icon"))
        {
            Sprite sprite = (Sprite) EditorGUILayout.ObjectField(item.Icon, typeof(Sprite), false);
            property.objectReferenceValue = sprite;
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawCrosshair(Item item)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Crosshair", GUILayout.Width(120));
        if (item.CrosshairDefinition?.Sprite != null)
        {
            GUILayout.Box(item.CrosshairDefinition.Sprite.texture, GUILayout.Width(60), GUILayout.Height(60));
        }
        else
        {
            EditorGUILayout.HelpBox("No Crosshair Selected", MessageType.Warning);
        }

        using (SerializedProperty property = serializedObject.FindProperty("_crosshairDefinition"))
        {
            CrosshairDefinition crosshairDefinition = (CrosshairDefinition) EditorGUILayout.ObjectField(
                item.CrosshairDefinition,
                typeof(CrosshairDefinition),
                false
            );
            property.objectReferenceValue = crosshairDefinition;
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawStatMods(Item item)
    {
        using (SerializedProperty statModsProperty = serializedObject.FindProperty("_statMods"))
        {
            // Loop through every Stat
            for (int i = 0; i < statModsProperty.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                // Create an X button to remove an array element at a given index
                if (GUILayout.Button("x", GUILayout.Width(20)))
                {
                    statModsProperty.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                    break;
                }

                // Get the current Stat we want to serialize
                SerializedProperty statMod = statModsProperty.GetArrayElementAtIndex(i);
                if (statMod != null)
                {
                    // Get the StatType and Value properties relative to the Stat property
                    SerializedProperty statTypeProperty = statMod.FindPropertyRelative("StatType");
                    SerializedProperty valueProperty = statMod.FindPropertyRelative("Value");

                    // Create an enum popup bound to our StatType property enum value
                    statTypeProperty.enumValueIndex = (int) (StatType) EditorGUILayout.EnumPopup(
                        (StatType) statTypeProperty.enumValueIndex,
                        GUILayout.Width(120)
                    );

                    EditorGUILayout.PropertyField(valueProperty, GUIContent.none, false);

                    serializedObject.ApplyModifiedProperties();
                }

                EditorGUILayout.EndHorizontal();
            }

            // A button to add a stat to an Item
            if (GUILayout.Button("+ Add Stat"))
            {
                // Insert a new UseAction into our array of UseActions
                statModsProperty.InsertArrayElementAtIndex(statModsProperty.arraySize);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }

    private void DrawActions(Item item)
    {
        using (SerializedProperty actionsProperty = serializedObject.FindProperty("_actions"))
        {
            // Loop through every UseAction in the array
            for (int i = 0; i < actionsProperty.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                // Create an X button to remove an array element at a given index
                if (GUILayout.Button("x", GUILayout.Width(20)))
                {
                    actionsProperty.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                    break;
                }

                // Get our UseAction as a SerializedProperty
                SerializedProperty action = actionsProperty.GetArrayElementAtIndex(i);
                if (action != null)
                {
                    // Get the UseMode and TargetComponent properties relative to the UseAction property
                    SerializedProperty useModeProperty = action.FindPropertyRelative("UseMode");
                    SerializedProperty targetComponentProperty = action.FindPropertyRelative("TargetComponent");

                    // Create an enum popup bound to our UseMode property enum value
                    useModeProperty.enumValueIndex = (int) (UseMode) EditorGUILayout.EnumPopup(
                        (UseMode) useModeProperty.enumValueIndex,
                        GUILayout.Width(80)
                    );

                    EditorGUILayout.PropertyField(targetComponentProperty, GUIContent.none, false);

                    serializedObject.ApplyModifiedProperties();
                }

                EditorGUILayout.EndHorizontal();
            }

            CreateAutoAssignButton(item, actionsProperty);
        }
    }

    private void CreateAutoAssignButton(Item item, SerializedProperty actionsProperty)
    {
        // A button to auto-assign ItemComponents that are not yet set up
        if (GUILayout.Button("Auto Assign Actions"))
        {
            // Determine what ItemComponents are already assigned in our UseActions
            List<ItemComponent> assignedItemComponents = new List<ItemComponent>();
            // We loop through our current list of UseActions
            for (int i = 0; i < actionsProperty.arraySize; i++)
            {
                // Get the current UseAction
                SerializedProperty action = actionsProperty.GetArrayElementAtIndex(i);
                if (action != null)
                {
                    // Get the ItemComponent on this UseAction
                    SerializedProperty targetComponentProperty = action.FindPropertyRelative("TargetComponent");
                    // Add it to our running list
                    ItemComponent assignedItemComponent = targetComponentProperty.objectReferenceValue as ItemComponent;
                    assignedItemComponents.Add(assignedItemComponent);
                }
            }

            // Loop through the list of ItemComponents are attached to the object
            foreach (ItemComponent itemComponent in item.GetComponentsInChildren<ItemComponent>())
            {
                // Find ones that are not in our list of already assigned ItemComponents
                if (!assignedItemComponents.Contains(itemComponent))
                {
                    // Insert a new UseAction into our array of UseActions
                    actionsProperty.InsertArrayElementAtIndex(actionsProperty.arraySize);
                    SerializedProperty targetAction = actionsProperty.GetArrayElementAtIndex(actionsProperty.arraySize - 1);
                    // Set it's ItemComponent to this attached ItemComponent
                    SerializedProperty targetItemComponent = targetAction.FindPropertyRelative("TargetComponent");
                    targetItemComponent.objectReferenceValue = itemComponent;
                    serializedObject.ApplyModifiedProperties();
                }
            }
        }
    }
}