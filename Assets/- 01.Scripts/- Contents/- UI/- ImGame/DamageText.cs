using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : Pool
{
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _alphaSpeed = 5.0f;
    [SerializeField] private float _destroyTime = 2.0f;

    private Vector3 _dir;

    private Color _alpha;
    private Text _damageText;
    private Transform _camera;
    private Coroutine _damageCor = null;

    void Awake()
    {
        _alpha = _damageText.color;
        _camera = Camera.main.transform;
    }

    private void StartDamageTextCor(int damage)
    {
        if (_damageCor != null)
        {
            StopCoroutine(_damageCor);
            _damageCor = null;
        }

        _damageCor = StartCoroutine(DamageTextFloating(damage));
    }

    IEnumerator DamageTextFloating(int damage)
    {
        _dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        _damageText.text = damage.ToString();
        while (_alpha.a >= 0)
        {
            transform.Translate(_dir * Time.deltaTime * _moveSpeed);
            _alpha.a = Mathf.Lerp(_alpha.a, 0, Time.deltaTime * _alphaSpeed);
            _damageText.color = _alpha;

            transform.LookAt(transform.position + _camera.rotation * Vector3.forward, _camera.rotation * Vector3.up);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
