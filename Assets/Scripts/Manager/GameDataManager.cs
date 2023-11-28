using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using static Define;

public class GameData
{
    public int haveGold = 0;
    
    public float maxExp = 10000;
    public float currentExp = 0;
    public int killScore = 0;
    
    public int playerLevel = 1;
    public float playerCurrentHp = 200;
    public float playerMaxHp = 200;
    public float playerMoveSpeed = 5.0f;
    public float fireDamage = 10.0f;
    public float plusStr = 1; // 스텟 업 시 곱할 데미지 수치
    public float plusHp = 1; //       ''     체력 수치
    public float plusDex = 1; //      ''     이속 수치
    
    public float attackDelay = 1f;
    
    public int fireLevel = 0;
    
    // 스킬들 레벨
    public int turretLevel = 0;
    public int grenadeLevel = 0;
    
    public int fireBallLevel = 0;
    public int iceBallLevel = 0;
    public int poisonBallLevel = 0;
    public int droneLevel = 0;
    public int arrowLevel = 0;
    public int iceLevel = 0;
    public int swordShieldLevel = 0;
    public int meteorLevel = 0;
    public int bounceBallLevel = 0;
    public int ReWindLevel = 0;

    // 스킬들 데미지
    public float turretDamage = 10.0f;
    public float grenadeDamage = 10.0f;
    
    public float fireBallDamage = 10.0f;
    public float iceBallDamage = 10.0f;
    public float poisonBallDamage = 10.0f;
    public float droneDamage = 10.0f;
    public float arrowDamage = 10.0f;
    public float bounceBallDamage = 10.0f;
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
    public int KillScore
    {
        get { return _gameData.killScore; }
        set { _gameData.killScore = value; }
    }
    public int HaveGold
    {
        get { return _gameData.haveGold; }
        set { _gameData.haveGold = value; }
    }
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
        get { return _gameData.playerCurrentHp; }
        set { _gameData.playerCurrentHp = value; }
    }
    public float PlayerMaxHp
    {
        get { return _gameData.playerMaxHp; }
        set { _gameData.playerMaxHp = value; }
    }

    public float PlayerMoveSpeed
    {
        get { return _gameData.playerMoveSpeed; }
        set { _gameData.playerMoveSpeed = value; }
    }
    public float PlusStr
    {
        get { return _gameData.plusStr; }
        set { _gameData.plusStr = value; }
    }
    public float PlusHp
    {
        get { return _gameData.plusHp; }
        set { _gameData.plusHp = value; }
    }
    public float PlusDex
    {
        get { return _gameData.plusDex; }
        set { _gameData.plusDex = value; }
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
    public int FireBallLevel
    {
        get { return _gameData.fireBallLevel; }
        set { _gameData.fireBallLevel = value; }
    }
    public int IceBallLevel
    {
        get { return _gameData.iceBallLevel; }
        set { _gameData.iceBallLevel = value; }
    }
    public int PoisonBallLevel
    {
        get { return _gameData.poisonBallLevel; }
        set { _gameData.poisonBallLevel = value; }
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
    public int SwordShieldLevel
    {
        get { return _gameData.swordShieldLevel; }
        set { _gameData.swordShieldLevel = value; }
    }
    public int MeteorLevel
    {
        get { return _gameData. meteorLevel; }
        set { _gameData.meteorLevel = value; }
    }
    public int BounceBallLevel
    {
        get { return _gameData.bounceBallLevel; }
        set { _gameData.bounceBallLevel = value; }
    }
    public int ReWind
    {
        get { return _gameData.ReWindLevel; }
        set { _gameData.ReWindLevel = value; }
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
    public float FireBallDamage
    {
        get { return _gameData.fireBallDamage; }
        set { _gameData.fireBallDamage = value; }
    }
    public float IceBallDamage
    {
        get { return _gameData.iceBallDamage; }
        set { _gameData.iceBallDamage = value; }
    }
    public float PoisonBallDamage
    {
        get { return _gameData.poisonBallDamage; }
        set { _gameData.poisonBallDamage = value; }
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
    public float BounceBallDamage
    {
        get { return _gameData.bounceBallDamage; }
        set { _gameData.bounceBallDamage = value; }
    }
}