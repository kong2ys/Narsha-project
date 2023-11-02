using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    private int level;
    private float _timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);
            
        if (_timer > spawnData[level].spawnTime)
        {
            _timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {   
        GameObject enemyObject = GameManager.instance.pool.Get(level);
        enemyObject.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.Initialize(spawnData[level]); 
    }
}

[System.Serializable]
public class SpawnData
{
    public string enemyName;
    public float spawnTime;
    public int health;
    public float speed;
}