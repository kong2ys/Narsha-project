using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public abstract class StructureState : MonoBehaviour
{
    protected StructureController structureController;

    [SerializeField] protected float _duration = 1f;
    [SerializeField] protected float _reDuration = 5f;
    private GameObject[] bossObjects;  // 보스 구조물 오브젝트 ([0]: fence / [1]: castle / [2]: portal)
    
    private void Awake()
    {
        structureController = MapManager.Instance.structureController;
        bossObjects = structureController.bossObj;
    }

    public abstract void OnSpawn();

    public abstract void OnDie();
    
    protected IEnumerator MoveStructure(int objectIndex, float duration, float targetY)
    {
        Vector3 startPosition = bossObjects[objectIndex].transform.position;
        float elapsed = 0f;

        while (elapsed < duration) 
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            Vector3 newPosition = new Vector3(bossObjects[objectIndex].transform.position.x,
                Mathf.LerpUnclamped(startPosition.y, targetY, t), bossObjects[objectIndex].transform.position.z);
            bossObjects[objectIndex].transform.position = newPosition;
            yield return null;
        }

        Vector3 finalPosition = new Vector3(bossObjects[objectIndex].transform.position.x, targetY, bossObjects[objectIndex].transform.position.z);
        bossObjects[objectIndex].transform.position = finalPosition;
    }
}
