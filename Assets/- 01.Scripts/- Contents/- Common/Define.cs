using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum PoolingKey
    {
        AKProjectile,
        CrowProjectile,
        AnchorProjectile,
        AxeProjectile,
        TrumpetProjectile,

        ItemAK,
        ItemCrow,
        ItemAnchor,
        ItemAxe,
        ItemTrumpet,

        ItemTester,

        Snake,
        Elephant,
        Skeleton,
        Mummy,
        Zombie,
        Yeti
    }

    public enum ObjectType
    {
        Weapon,
        Projectile,
        Enemy,
        Coin
    }


    public enum PoolingNum
    {
        Projectile_bullet,
        Projectile_Scare,
        Projectile_ball,
        Pool_1,
        Pool_2,
        Text,
        zoombie,
        skeleton,
        yeti,
        mummy,
        Elephant,
        Spider,
        Coin
    }

    public enum WeaponType
    {
        Ak,
        Scarecrow,
        Launcher,
        Chaining
    }

    public enum PageType
    {
        Menu,
        Stage,
        Item,
        Ranking,
        Creator
    }
}
