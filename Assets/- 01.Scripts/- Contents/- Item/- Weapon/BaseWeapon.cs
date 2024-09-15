using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;

public class BaseWeapon : Pool
{
    [SerializeField] protected BaseProjectile _bullet;
    [SerializeField] protected Transform _startPos;

    #region Weapon Data 
    protected int _bulletCapacity;
    protected float _reloadTime;
    protected float _intervalTime;
    protected int _shotCount;
    #endregion

    private Coroutine _autoShotCoroutine = null;
    protected WaitForSeconds _reloadWait;
    protected WaitForSeconds _intervalWait;

    public bool IsDroppable { get; private set; } = true;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        if (IsDroppable)
        {
            SetAutoShotSettings();
            RegisterBullet();
        }

        ObjType = Define.ObjectType.Weapon;
    }


    public void SetAutoShotSettings()
    {
        _reloadWait = new WaitForSeconds(_reloadTime);
        _intervalWait = new WaitForSeconds(_intervalTime);
    }

    public virtual void RegisterBullet()
    {
        ObjectPooling.Instance.RegisterPooling(_bullet.Key, _bullet, _bulletCapacity);
    }

    private void Update()
    {
        if (IsDroppable)
        {
            transform.Rotate(Vector3.up, 60f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDroppable && other.GetComponent<MainPlayer>() != null)
        {
            AttachWeaponToPlayer(other);
        }
    }

    public virtual void AttachWeaponToPlayer(Collider player)
    {
        IsDroppable = false;
        transform.SetParent(player.transform);
        ActivateWeapon();
    }

    public virtual void ActivateWeapon()
    {
        StartAutoShooting();
    }

    public virtual void Shoot()
    {
        BaseProjectile bulet =  ObjectPooling.Instance.SpawnWithParent(_bullet.Key, _startPos) as BaseProjectile;
        bulet.Shoot(_startPos.forward);
    }

    private void StartAutoShooting()
    {
        if (_autoShotCoroutine != null)
        {
            StopCoroutine(_autoShotCoroutine);
        }

        _autoShotCoroutine = StartCoroutine(AutoShootingCoroutine());
    }

    public virtual IEnumerator AutoShootingCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < _shotCount; i++)
            {

                Shoot();
                yield return _intervalWait;
            }

            yield return _reloadWait;
        }
    }
}
