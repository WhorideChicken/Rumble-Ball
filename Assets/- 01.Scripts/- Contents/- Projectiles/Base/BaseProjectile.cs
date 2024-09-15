using UnityEngine;
using System.Collections;

public abstract class BaseProjectile : Pool
{
    protected ProjectileMover _mover;
    public float Damage { get; protected set; }
    public float Speed { get; protected set; }

    protected BoxCollider Collider { get; private set; }

    private Coroutine _despawnCoroutine;
    private static readonly WaitForSeconds _waitDespawnSec = new WaitForSeconds(2.0f);

    protected virtual void Awake()
    {
        Collider = GetComponent<BoxCollider>();
        _mover = GetComponent<ProjectileMover>();
    }

    public virtual void Shoot(Vector3 direction)
    {
        ResetTransform();
        transform.parent = null;
        StartMovement(direction.normalized);
        StartDespawnCountdown();
    }

    protected virtual void StartMovement(Vector3 direction)
    {
        _mover.StartMovement(transform, direction, Speed);
    }

    public virtual void ResetTransform()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public virtual void DeSpawn()
    {
        _mover.StopMovement();
        StopDespawnCountdown();
        ResetTransform();
        ObjectPooling.Instance.DeSpawn(Key, this);
    }

    private void StartDespawnCountdown()
    {
        if (_despawnCoroutine != null)
        {
            StopCoroutine(_despawnCoroutine);
        }

        _despawnCoroutine = StartCoroutine(DespawnCoroutine());
    }

    private IEnumerator DespawnCoroutine()
    {
        yield return _waitDespawnSec;
        DeSpawn();
    }

    private void StopDespawnCountdown()
    {
        if (_despawnCoroutine != null)
        {
            StopCoroutine(_despawnCoroutine);
            _despawnCoroutine = null;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BaseEnemy>(out BaseEnemy enemy))
        {
            enemy.TakeDamage(CalculateDamage());
            DeSpawn();
        }
    }

    protected abstract float CalculateDamage();
}