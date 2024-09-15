using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : Pool
{
    [SerializeField] private PlayerPosition _targetPosition;
    [SerializeField] private Transform _damageCanvas;
    [SerializeField] private SkinnedMeshRenderer _dissolveMaterial;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Coroutine _followCoroutine;
    private Coroutine _dissolveCoroutine;
    private Coroutine _invincibilityCoroutine;

    private Material _dissolveMaterialInstance;
    private float _hp = 100;
    private static readonly Vector3 DamagePosition = new Vector3(0, 4.5f, 0);
    private static readonly WaitForSeconds DissolveInterval = new WaitForSeconds(0.01f);
    private static readonly WaitForSeconds InvincibilityDuration = new WaitForSeconds(0.75f);
    private const float FollowUpdateInterval = 0.2f;
    private const int MaxHP = 100;
    private const int DamageAmount = 2;
    private bool _isInvincible = false;

    public int Damage => DamageAmount;

    private void Awake()
    {
        _dissolveMaterialInstance = _dissolveMaterial?.materials[0];
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public override void Init()
    {
        base.Init();
        ResetEnemy();
        StartFollowCoroutine();
        ObjType = Define.ObjectType.Enemy;
    }


    private void OnDisable()
    {
        StopDissolve();
        ResetEnemy();
    }

    private void ResetEnemy()
    {
        _hp = MaxHP;
        _dissolveMaterialInstance.SetFloat("Dissolve", -1f);
        _navMeshAgent.enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        _animator.SetBool("isRunning", true);
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            SetDead();
        }
        else
        {
            // StartKnockBack(); // Uncomment if knockback is implemented
        }
    }

    private void SetDead()
    {
        StopFollowCoroutine();
        EnterDeadState();
        StartDissolveCoroutine();
    }

    private void EnterDeadState()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<CapsuleCollider>().enabled = false;

        int randomAnim = UnityEngine.Random.Range(0, 10) % 2;
        _animator.SetBool(randomAnim == 0 ? "isDead1" : "isDead2", true);
    }

    private void StopFollowCoroutine()
    {
        if (_followCoroutine != null)
        {
            StopCoroutine(_followCoroutine);
            _followCoroutine = null;
            _navMeshAgent.enabled = false;
        }
    }

    private void StartFollowCoroutine()
    {
        StopFollowCoroutine();
        _followCoroutine = StartCoroutine(FollowPlayerCoroutine());
    }

    private IEnumerator FollowPlayerCoroutine()
    {
        while (true)
        {
            if (gameObject.activeSelf)
            {
                _navMeshAgent.SetDestination(_targetPosition.Value);
                yield return new WaitForSeconds(FollowUpdateInterval);
            }
            yield return null;
        }
    }

    private void StopDissolve()
    {
        if (_dissolveCoroutine != null)
        {
            StopCoroutine(_dissolveCoroutine);
            _dissolveCoroutine = null;
        }

        _dissolveMaterialInstance.SetFloat("Dissolve", -1f);
    }

    private void StartDissolveCoroutine()
    {
        StopDissolve();
        _dissolveCoroutine = StartCoroutine(DissolveCoroutine());
    }

    private IEnumerator DissolveCoroutine()
    {
        float dissolveAmount = _dissolveMaterialInstance.GetFloat("Dissolve");

        while (dissolveAmount < 1.0f)
        {
            dissolveAmount += 0.01f;
            _dissolveMaterialInstance.SetFloat("Dissolve", dissolveAmount);
            yield return DissolveInterval;
        }

        ReturnToPool();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_isInvincible && other.GetComponent<MainPlayer>() != null)
        {
            _isInvincible = true;
            other.GetComponent<MainPlayer>().GetDamage(Damage);
            StartInvincibilityCoroutine();
        }
    }

    private void StartInvincibilityCoroutine()
    {
        if (_invincibilityCoroutine != null)
        {
            StopCoroutine(_invincibilityCoroutine);
        }

        _invincibilityCoroutine = StartCoroutine(InvincibilityCoroutine());
    }

    private IEnumerator InvincibilityCoroutine()
    {
        yield return InvincibilityDuration;
        _isInvincible = false;
    }

    private void ReturnToPool()
    {
        ObjectPooling.Instance.DeSpawn(Key, this);
    }
}
