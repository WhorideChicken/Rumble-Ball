using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorProjectile : BaseProjectile
{
    public override void Init()
    {
        base.Init();
        Key = Define.PoolingKey.AnchorProjectile;
        ObjType = Define.ObjectType.Projectile;
        Damage = 10f;
        Speed = 20f;
    }

    protected override float CalculateDamage()
    {
        return Damage;
    }

    protected override void StartMovement(Vector3 direction)
    {
        //TODO : Anchor 
    }

}
