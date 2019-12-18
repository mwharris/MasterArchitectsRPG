using TMPro;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    [SerializeField] private CrosshairDefinition _crosshairDefinition;
    [SerializeField] private UseAction[] _actions = new UseAction[0];
    [SerializeField] private Sprite _icon;

    public CrosshairDefinition CrosshairDefinition => _crosshairDefinition;
    public UseAction[] Actions => _actions;
    public Sprite Icon => _icon;

    private bool _wasPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (_wasPickedUp)
            return;

        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.Pickup(this);
            _wasPickedUp = true;
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
        DrawActions();
    }

    private void DrawIcon(Item item)
    {
        EditorGUILayout.BeginHorizontal("Icon");
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
        EditorGUILayout.BeginHorizontal("Crosshair");
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

    private void DrawActions()
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
        }
    }
}