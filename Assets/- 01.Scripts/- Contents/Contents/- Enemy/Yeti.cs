using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeti : BaseEnemy
{
    public override void Init()
    {
        base.Init();
        Key = Define.PoolingKey.Yeti;
    }
}
