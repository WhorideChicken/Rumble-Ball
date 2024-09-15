using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAK : BaseWeapon
{
    public override void Init()
    {
        Key = Define.PoolingKey.ItemAK;
        _bulletCapacity = 10;
        _reloadTime = 2.0f;
        _intervalTime = 0.2f;
        _shotCount = 3;
        base.Init();
    }

}
