using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public abstract class StructureState : MonoBehaviour
{
    private StructureController _structureController;

    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _reDuration = 5f;

    private void Awake()
    {
        _structureController = MapManager.Instance.structureController;
    }
    

    public abstract void OnSpawn();

    public abstract void OnDie();
    
    public IEnumerator MoveStructure(int objectIndex, float duration, float targetY)
    {
        Vector3 startPosition = bossObj[objectIndex].transform.position;
        float elapsed = 0f;

        while (elapsed < duration) 
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            Vector3 newPosition = new Vector3(bossObj[objectIndex].transform.position.x,
                Mathf.LerpUnclamped(startPosition.y, targetY, t), bossObj[objectIndex].transform.position.z);
            bossObj[objectIndex].transform.position = newPosition;
            yield return null;
        }

        Vector3 finalPosition = new Vector3(bossObj[objectIndex].transform.position.x, targetY, bossObj[objectIndex].transform.position.z);
        bossObj[objectIndex].transform.position = finalPosition;
    }
}
