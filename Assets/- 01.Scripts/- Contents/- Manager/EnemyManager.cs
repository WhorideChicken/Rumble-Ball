using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private PlayerPosition _position;
 
    public BaseEnemy[] Enemies;
    public Pool Coin;

    private Coroutine _spawnCor = null;

    public int TotalEnemyCount = 100;
    private int _currntEnemyCount = 0;

    private void Start()
    {
        RegistEnemyPool();
    }

    private void RegistEnemyPool()
    {
        int average = TotalEnemyCount / Enemies.Length;
        for (int i = 0; i < Enemies.Length; ++i)
        {
            ObjectPooling.Instance.RegisterPooling(Enemies[i].Key, Enemies[i], average);
        }

        RegistCoinPool();
        StartSapwnCor();
    }

    private void RegistCoinPool()
    {
       // Coin.Count = TotalEnemyCount;
       // BaseObjectPool.Instance.RegistEnemyPooling(Coin);
    }

    private void StartSapwnCor()
    {
        if(_spawnCor != null)
        {
            StopCoroutine(_spawnCor);
            _spawnCor = null;
        }

        _spawnCor = StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(1);
        while(_currntEnemyCount < TotalEnemyCount)
        {
            int random = Random.Range(0, Enemies.Length);

            GameObject ob = ObjectPooling.Instance.Spawn(Enemies[random].Key)?.gameObject;
            if (ob!=null)
            {
                ob.transform.position = SpawnEnemy();

                _currntEnemyCount++;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }


    private Vector3 SpawnEnemy()
    {
        float Radius = Random.Range(10,20);
        float a = _position.Value.x;
        float b = _position.Value.z;

        float x = Random.Range(-Radius + a, Radius + a);
        float z_b = Mathf.Sqrt(Mathf.Pow(Radius, 2) - Mathf.Pow(x - a, 2));
        z_b *= Random.Range(0, 2) == 0 ? -1 : 1;

        float z = z_b + b;

        return new Vector3(x, 0, z);
    }
}
