using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectDropdown;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    public int circleCount;

    [ScriptableObjectDropdown] public LevelTheme levelTheme;
}
