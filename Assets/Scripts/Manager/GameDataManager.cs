using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using static Define;

public class GameData
{
    public int playerLevel = 1;
    public float playerHp = 200;
    public float attackDelay = 1f;
    
    // 스킬들 레벨
    public int fireLevel = 0;
    
    public int turretLevel = 0;
    
    public int axeLevel = 0;
    public int droneLevel = 0;
    public int bombLevel = 0;
    
    // 스킬들 데미지
    public float fireDamage = 10.0f;
    
    public float turretDamage = 10.0f;
    public float bombDamage = 10.0f;
    
    public float axeDamage = 10.0f;
    public float droneDamage = 10.0f;
}

public class GameDataManager
{
    private static GameDataManager _instance;
    
    public static GameDataManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameDataManager();
            return _instance;
        }
    }

    GameData _gameData = new GameData();

    
    public int PlayerLevel
    {
        get { return _gameData.playerLevel; }
        set { _gameData.playerLevel = value; }
    }
    public float PlayerHp
    {
        get { return _gameData.playerHp; }
        set { _gameData.playerHp = value; }
    }
    public float AttackDelay
    {
        get { return _gameData.attackDelay; }
        set { _gameData.attackDelay = value; }
    }
    
    
    // 스킬 레벨
    public int FireLevel
    {
        get { return _gameData.fireLevel;  }
        set { _gameData.fireLevel = value;  }
    }
    
    public int TurretLevel
    {
        get { return _gameData.turretLevel; }
        set { _gameData.turretLevel = value; }
    }
    public int BombLevel
    {
        get { return _gameData.bombLevel; }
        set { _gameData.bombLevel = value; } 
    }

    public int AxeLevel
    {
        get { return _gameData.axeLevel; }
        set
        {
            _gameData.axeLevel = value;
        }
    }
    public int DroneLevel
    {
        get { return _gameData.droneLevel; }
        set { _gameData.droneLevel = value; }
    }
    
    
    // 스킬 데미지
    public float FireDamage
    {
        get { return _gameData.fireDamage; }
        set { _gameData.fireDamage = value; }
    }
    
    public float TurretDamage
    {
        get { return _gameData.turretDamage; }
        set { _gameData.turretDamage = value; }
    }
    public float BombDamage
    {
        get { return _gameData.bombDamage; }
        set { _gameData.bombDamage = value; }
    }
    
    public float AxeDamage
    {
        get { return _gameData.axeDamage; }
        set { _gameData.axeDamage = value; }
    }
    public float DroneDamage
    {
        get { return _gameData.droneDamage; }
        set { _gameData.droneDamage = value; }
    }
}