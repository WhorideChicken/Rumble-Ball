using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : BaseEnemy
{
    public override void Init()
    {
        base.Init();
        Key = Define.PoolingKey.Skeleton;
    }
}
