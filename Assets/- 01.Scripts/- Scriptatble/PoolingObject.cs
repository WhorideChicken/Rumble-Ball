using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Global Variables/Enemy")]
public class PoolingObject: ScriptableObject
{
    public Pool Object;
    public Define.ObjectType ObjType;
    public Define.PoolingKey Key;
}
