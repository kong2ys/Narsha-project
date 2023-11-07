using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameDataManager gmd;
    
    public SpawnData spawnData;
    
    [Header("# Game Control")]

    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int level;
    public int kill;
    public float exp;
    
    [Header("# Game Object")]
    public PoolManager pool;
    public PlayerController player;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
        
        exp = GameDataManager.Instance.CurrentExp;
        
    }
    
}
