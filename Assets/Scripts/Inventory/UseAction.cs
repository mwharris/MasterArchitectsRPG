using System;
using UnityEngine;

[Serializable]
public struct UseAction
{
    public UseMode UseMode;
    public ItemComponent TargetComponent;
}