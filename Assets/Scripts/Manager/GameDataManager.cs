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
    public int axeLevel = 0;
    public int turretLevel = 0;
    public int droneLevel = 0;
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
    
    public int FireLevel
    {
        get { return _gameData.fireLevel;  }
        set { _gameData.fireLevel = value;  }
    }
    public int AxeLevel
    {
        get { return _gameData.axeLevel; }
        set
        {
            _gameData.axeLevel = value;
        }
    }
    public int TurretLevel
    {
        get { return _gameData.turretLevel; }
        set { _gameData.turretLevel = value; }
    }
    public int DroneLevel
    {
        get { return _gameData.droneLevel; }
        set { _gameData.droneLevel = value; }
    }
}