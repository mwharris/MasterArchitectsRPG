using System.Collections;
using UnityEngine;

public interface IItem
{
    Sprite Icon { get; }
    Transform transform { get; }
    GameObject gameObject { get; }
    CrosshairDefinition CrosshairDefinition { get; }
    UseAction[] Actions { get; }
    StatMod[] StatMods { get; }
}