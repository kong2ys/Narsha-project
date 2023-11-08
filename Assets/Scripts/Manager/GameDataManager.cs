using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using static Define;

public class GameData
{
    public float maxExp = 10000;
    public float currentExp = 0;
    
    public int playerLevel = 1;
    public float playerHp = 200;
    
    public float attackDelay = 1f;
    
    public int fireLevel = 0;
    public float fireDamage = 10.0f;
    
    // 스킬들 레벨
    public int turretLevel = 0;
    public int grenadeLevel = 0;
    
    public int axeLevel = 0;
    public int droneLevel = 0;
    public int arrowLevel = 0;
    public int iceLevel = 0;

    // 스킬들 데미지
    public float turretDamage = 10.0f;
    public float grenadeDamage = 10.0f;
    
    public float axeDamage = 10.0f;
    public float droneDamage = 10.0f;
    public float arrowDamage = 10.0f;
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

    // 플레이어
    public float MaxExp
    {
        get { return _gameData.maxExp; }
        set { _gameData.maxExp = value; }
    }
    public float CurrentExp
    {
        get { return _gameData.currentExp; }
        set { _gameData.currentExp = value; }
    }
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
    public int FireLevel
    {
        get { return _gameData.fireLevel;  }
        set { _gameData.fireLevel = value;  }
    }
    public float FireDamage
    {
        get { return _gameData.fireDamage; }
        set { _gameData.fireDamage = value; }
    }

    // 엑티브 스킬 레벨
    public int TurretLevel
    {
        get { return _gameData.turretLevel; }
        set { _gameData.turretLevel = value; }
    }
    public int GrenadeLevel
    {
        get { return _gameData.grenadeLevel; }
        set { _gameData.grenadeLevel = value; } 
    }

    // 패시브 스킬 레벨
    public int AxeLevel
    {
        get { return _gameData.axeLevel; }
        set { _gameData.axeLevel = value; }
    }
    public int DroneLevel
    {
        get { return _gameData.droneLevel; }
        set { _gameData.droneLevel = value; }
    }
    public int ArrowLevel
    {
        get { return _gameData.arrowLevel; }
        set { _gameData.arrowLevel = value; }
    }
    public int IceLevel
    {
        get { return _gameData.iceLevel; }
        set { _gameData.iceLevel = value; }
    }
    
    // 엑티브 스킬 데미지
    public float TurretDamage
    {
        get { return _gameData.turretDamage; }
        set { _gameData.turretDamage = value; }
    }
    public float GrenadeDamage
    {
        get { return _gameData.grenadeDamage; }
        set { _gameData.grenadeDamage = value; }
    }
    
    // 패시브 스킬 데미지
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
    public float ArrowDamage
    {
        get { return _gameData.arrowDamage; }
        set { _gameData.arrowDamage = value; }
    }
}