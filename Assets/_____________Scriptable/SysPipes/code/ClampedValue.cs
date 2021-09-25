using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClampedValue", menuName = "Pipes/ClampedValue")]
public class ClampedValue : ScriptableObject
{
    public float maxValue;
    public float value;
}
