using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public class BalttanState : StructureState
{
    public override void OnSpawn()
    {
        StartCoroutine(MoveStructure(0, _duration, 1f));
        StartCoroutine(MoveStructure(2, _duration, 1f));
        structureController.isBalltanLive = true;
        MapManager.Instance.poolManager.SetActive(false);
    }

    public override void OnDie()
    {
        StartCoroutine(MoveStructure(0, _reDuration, -100f));
        structureController.isBalltanLive = false;
        MapManager.Instance.poolManager.SetActive(true);
    }
}
