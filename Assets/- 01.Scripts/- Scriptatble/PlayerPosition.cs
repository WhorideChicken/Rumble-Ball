using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Global Variables/PlayerPosition")]
public class PlayerPosition : ScriptableObject
{
    public Vector3 Value { get; set; }
}
