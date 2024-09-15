using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAxe : BaseWeapon
{
    public override void Init()
    {
        Key = Define.PoolingKey.ItemAxe;
        _bulletCapacity = 2;
        _reloadTime = 1.0f;
        _intervalTime = 1.0f;
        _shotCount = 1;
        base.Init();
    }
}
