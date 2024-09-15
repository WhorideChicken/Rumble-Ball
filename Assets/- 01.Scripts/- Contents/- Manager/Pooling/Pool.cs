using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public Define.PoolingKey Key;
    public Define.ObjectType ObjType;

    public virtual void Init()
    {
    }
}
