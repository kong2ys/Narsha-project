using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] mappingObj;     // 9개 타일 오브젝트
    public GameObject fence;            // 발탄 보스전 구조물 오브젝트
    public GameObject castle;           // boss2 보스전 구조물 오브젝트
    public GameObject portal;           // portal 구조물 오브젝트
    public GameObject player;           // player 오브젝트
    public GameObject balltan;          // 발탄 오브젝트
    // public GameObject boss2;            // boss2 오브젝트
    public GameObject poolManager;
    public float tileSize;              // 타일 1개 길이

    private float _dirFront;     // 앞 타일 거리
    private float _dirBack;      // 뒤 타일 거리
    private float _dirRight;     // 오른쪽 타일 거리  
    private float _dirLeft;      // 왼쪽 타일 거리

    private float _posX;         // 플레이어 오브젝트 x값 위치
    private float _posZ;         // 플레이어 오브젝트 z값 위치
    private float _movePos;      // 타일 이동값

    private float _duration = 1f;       // 구조물 등장 소요 시간
    private float _reDuration = 5f;     // 구조물 사라짐 소요 시간
    private float _targetY = 0;         // 구조물 올라오는 위치
    private float _castleTargetY = 20;  // castle 구조물 올라오는 위치
    private float _reTargetY = -100;    // 구조물 내려가는 위치

    private bool _isBalltanLive = false;         // 발탄 생존 여부 플래그
    private bool _isBoss2Live;           // boss2 생존 여부 플래그

    private Balltan _balltanScript;      // 발탄 스크립트 참조
    // private Boss2 _boss2Script;          // boss2 스크립트 참조
    private GameDataManager _gmScript;

    private bool _isFence = false;       // fance 등장 상태 플래그
    private bool _isCastle = false;      // castle 등장 상태 플래그
    private bool _isPortal = false;      // portal 등장 상태 플래그
    
    private void Start()
    {
        _dirFront = tileSize / 2;
        _dirBack = tileSize / 2 * -1;
        _dirRight = tileSize / 2;
        _dirLeft = tileSize / 2 * -1;

        _movePos = tileSize * 3;     // 3 * 3 타일 => 타일 세 칸 넓이 이동
        
        // Balltan _balltanScript = balltan.GetComponent<Balltan>();
        // Boss2 _boss2Script = boss2.GetComponent<Boss2>();
    }
    
    void Update()
    {
        // 보스 생존 플래그 설정
        // _isBalltanLive = _balltanScript.isAlive;
        // _isBoss2Live = _boss2Script.isAlive;

        if (GameDataManager.Instance.KillScore >= 5 || Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("100");
            StartCoroutine(MoveFence(fence));
            StartCoroutine(MovePortal(portal));
            _isBalltanLive = true;
            poolManager.SetActive(false);
        }
        
        if (_isBalltanLive && !_isFence)
        {
            StartCoroutine(MoveFence(fence));
            _isFence = true;
        }
        else if (!_isBalltanLive && _isFence)
        {
            StartCoroutine(ReMoveFence(fence));
            _isFence = false;
        }

        if (_isBoss2Live && !_isCastle)
        {
            StartCoroutine(MoveCastle(castle));
            _isCastle = true;
        }
        else if (!_isBoss2Live && _isCastle)
        {
            StartCoroutine(ReMoveFence(castle));
            _isCastle = false;
        }
        
        if (_isPortal)
        {
            StartCoroutine(MovePortal(portal));
            _isPortal = false;
        }
        
        // 보스 생존 시 맵 이동 제한
        if (_isBalltanLive||_isBoss2Live)
        {
             return;
        }
        
        _posX = player.gameObject.transform.position.x;
        _posZ = player.gameObject.transform.position.z;
        
        // 지정해둔 타일 거리보다 더 이동했으면 타일 이동 후 위치에 맞춰서 거리값 변경
        
        if (_posZ >= _dirFront)
        {
            FrontMoveMap();
            _dirFront += tileSize;
            _dirBack += tileSize;
        }

        if (_posZ <= _dirBack)
        {
            BackMoveMap();
            _dirFront -= tileSize;
            _dirBack -= tileSize;
        }

        if (_posX >= _dirRight)
        {
            RightMoveMap();
            _dirRight += tileSize;
            _dirLeft += tileSize;
        }

        if (_posX <= _dirLeft)
        {
            LeftMoveMap();
            _dirRight -= tileSize;
            _dirLeft -= tileSize;
        }
    }
    
    void FrontMoveMap()
    {
        Swap(mappingObj, 6, 3, 0);
        Swap(mappingObj, 7, 4, 1);
        Swap(mappingObj, 8, 5, 2);
        mappingObj[0].transform.position += new Vector3(0, 0, _movePos);
        mappingObj[1].transform.position += new Vector3(0, 0, _movePos);
        mappingObj[2].transform.position += new Vector3(0, 0, _movePos);
        fence.transform.position += new Vector3(0, 0, tileSize);
        castle.transform.position += new Vector3(0, 0, tileSize);
        portal.transform.position += new Vector3(0, 0, tileSize);
    }
    
    void BackMoveMap()
    {
        Swap(mappingObj, 0, 3, 6);
        Swap(mappingObj, 1, 4, 7);
        Swap(mappingObj, 2, 5, 8);
        mappingObj[6].transform.position += new Vector3(0, 0, -_movePos);
        mappingObj[7].transform.position += new Vector3(0, 0, -_movePos);
        mappingObj[8].transform.position += new Vector3(0, 0, -_movePos);
        fence.transform.position += new Vector3(0, 0, -tileSize);
        castle.transform.position += new Vector3(0, 0, -tileSize);
        portal.transform.position += new Vector3(0, 0, -tileSize);
    }

    void RightMoveMap()
    {
        Swap(mappingObj, 0, 1, 2);
        Swap(mappingObj, 3, 4, 5);
        Swap(mappingObj, 6, 7, 8);
        mappingObj[2].transform.position += new Vector3(_movePos, 0, 0);
        mappingObj[5].transform.position += new Vector3(_movePos, 0, 0);
        mappingObj[8].transform.position += new Vector3(_movePos, 0, 0);
        fence.transform.position += new Vector3(tileSize, 0, 0);
        castle.transform.position += new Vector3(tileSize, 0, 0);
        portal.transform.position += new Vector3(tileSize, 0, 0);
    }

    void LeftMoveMap()
    {
        Swap(mappingObj, 2, 1, 0);
        Swap(mappingObj, 5, 4, 3);
        Swap(mappingObj, 8, 7, 6);
        mappingObj[0].transform.position += new Vector3(-_movePos, 0, 0);
        mappingObj[3].transform.position += new Vector3(-_movePos, 0, 0);
        mappingObj[6].transform.position += new Vector3(-_movePos, 0, 0);
        fence.transform.position += new Vector3(-tileSize, 0, 0);
        castle.transform.position += new Vector3(-tileSize, 0, 0);
        portal.transform.position += new Vector3(-tileSize, 0, 0);
    }
    
    void Swap(GameObject[] mapTile, int i, int j, int k)
    {
        // ex) 오른쪽 이동 시 맨 윗줄 타일이 [0][1][2] -> [1][2][0]으로 변경되는 방식
        
        GameObject test = mapTile[i];
        mapTile[i] = mapTile[j];
        mapTile[j] = mapTile[k];
        mapTile[k] = test;
    }
    
    IEnumerator MoveFence(GameObject fence)
    {
        Vector3 startPosition = fence.transform.position;
        float elapsed = 0f;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _duration;
            Vector3 newPosition = new Vector3(fence.transform.position.x,
                Mathf.LerpUnclamped(startPosition.y, _targetY, t), fence.transform.position.z);
            fence.transform.position = newPosition;
            yield return null;
        }

        Vector3 finalPosition = new Vector3(fence.transform.position.x, _targetY, fence.transform.position.z);
        fence.transform.position = finalPosition;
    }
    
    IEnumerator ReMoveFence(GameObject fence)
    {
        Vector3 startPosition = fence.transform.position;
        float elapsed = 0f;

        while (elapsed < _reDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _duration;
            Vector3 newPosition = new Vector3(fence.transform.position.x,
                Mathf.LerpUnclamped(startPosition.y, _reTargetY, t), fence.transform.position.z);
            fence.transform.position = newPosition;
            yield return null;
        }

        Vector3 finalPosition = new Vector3(fence.transform.position.x, _reTargetY, fence.transform.position.z);
        fence.transform.position = finalPosition;
    }

    IEnumerator MoveCastle(GameObject castle)
    {
        Vector3 startPosition = castle.transform.position;
        float elapsed = 0f;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _duration;
            Vector3 newPosition = new Vector3(castle.transform.position.x,
                Mathf.LerpUnclamped(startPosition.y, _castleTargetY, t), castle.transform.position.z);
            castle.transform.position = newPosition;
            yield return null;
        }

        Vector3 finalPosition = new Vector3(castle.transform.position.x, _castleTargetY, castle.transform.position.z);
        castle.transform.position = finalPosition;
    }
    
    IEnumerator ReMoveCastle(GameObject castle)
    {
        Vector3 startPosition = castle.transform.position;
        float elapsed = 0f;

        while (elapsed < _reDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _duration;
            Vector3 newPosition = new Vector3(castle.transform.position.x,
                Mathf.LerpUnclamped(startPosition.y, _reTargetY, t), castle.transform.position.z);
            castle.transform.position = newPosition;
            yield return null;
        }

        Vector3 finalPosition = new Vector3(castle.transform.position.x, _reTargetY, castle.transform.position.z);
        castle.transform.position = finalPosition;
    }
    
    IEnumerator MovePortal(GameObject portal)
    {
        Vector3 startPosition = portal.transform.position;
        float elapsed = 0f;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _duration;
            Vector3 newPosition = new Vector3(portal.transform.position.x,
                Mathf.LerpUnclamped(startPosition.y, _targetY, t), portal.transform.position.z);
            portal.transform.position = newPosition;
            yield return null;
        }

        Vector3 finalPosition = new Vector3(portal.transform.position.x, _targetY, portal.transform.position.z);
        portal.transform.position = finalPosition;
    }
    
}

