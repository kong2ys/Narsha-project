using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public class BalttanState : MonoBehaviour, StructureState
{
    private StructureController _structureController;
    
    private void Awake()
    {
         _structureController = MapManager.Instance.structureController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSpawn()
    {
        StartCoroutine(_structureController.MoveStructure(0, _structureController.duration, 1f));
        StartCoroutine(_structureController.MoveStructure(2, _structureController.duration, 1f));
        MapManager.isBalltanLive = true;
        
        StartCoroutine(MoveStructure(bossObj[0], _duration, _targetY));
        StartCoroutine(MoveStructure(bossObj[2],_duration ,_targetY));
        _isBalltanLive = true;
        MapManager.Instance.poolManager.SetActive(false);
    }

    public void OnDie()
    {
        
    }
}
