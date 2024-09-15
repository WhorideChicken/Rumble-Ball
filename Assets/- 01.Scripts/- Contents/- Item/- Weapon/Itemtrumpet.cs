using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemtrumpet : BaseWeapon
{
    public override void Init()
    {
        Key = Define.PoolingKey.ItemTrumpet;
        _bulletCapacity = 1;
        _reloadTime = 2.0f;
        _intervalTime = 0.5f;
        _shotCount = 1;
        base.Init();
    }
}
