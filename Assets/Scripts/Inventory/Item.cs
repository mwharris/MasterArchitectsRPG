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

        SerializedProperty sIcon = serializedObject.FindProperty("_icon");

        Rect iconRect = EditorGUILayout.BeginHorizontal("Testing");
        EditorGUILayout.PropertyField(sIcon, new GUIContent("Icon"), GUILayout.Height(20));
        GUILayout.Box(item.Icon.texture, GUILayout.Width(60), GUILayout.Height(60));
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
        
        serializedObject.ApplyModifiedProperties();
    }
}