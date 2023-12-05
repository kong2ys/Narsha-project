using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] mappingObj; // 9개 타일 오브젝트
    public GameObject[] bossObj;    // 보스 구조물 오브젝트 ([0]: fence / [1]: castle / [2]: portal)
    public GameObject player; // player 오브젝트

    public GameObject balltan;      // 발탄 오브젝트
    // public GameObject boss2;            // boss2 오브젝트
    public GameObject poolManager;  // poolManager 오브젝트
    
    public float tileSize; // 타일 1개 길이

    private float _dirFront; // 앞 타일 거리
    private float _dirBack;  // 뒤 타일 거리
    private float _dirRight; // 오른쪽 타일 거리  
    private float _dirLeft;  // 왼쪽 타일 거리

    private float _posX;    // 플레이어 오브젝트 x값 위치
    private float _posZ;    // 플레이어 오브젝트 z값 위치
    private float _movePos; // 타일 이동값

    private float _duration = 1f;       // 구조물 등장 소요 시간
    private float _reDuration = 5f;     // 구조물 사라짐 소요 시간
    private float _targetY = 0;         // 구조물 올라오는 위치
    private float _castleTargetY = 20;  // castle 구조물 올라오는 위치
    private float _reTargetY = -100;    // 구조물 내려가는 위치

    private bool _isBalltanLive = false; // 발탄 생존 여부 플래그
    private bool _isBoss2Live = false;   // boss2 생존 여부 플래그

    private Balltan _balltanScript;    // 발탄 스크립트 참조
    // private Boss2 _boss2Script;     // boss2 스크립트 참조
    private GameDataManager _gmScript; // 게임 데이터 매니저 참조

    private bool _isFence = false;  // fence 등장 상태 플래그
    private bool _isCastle = false; // castle 등장 상태 플래그
    private bool _isPortal = false; // portal 등장 상태 플래그

    private int[] _frontSwap;
    private int[] _backSwap;
    private int[] _rightSwap;
    private int[] _leftSwap;

    private int[] _frontMov;
    private int[] _backMov;
    private int[] _rightMov;
    private int[] _leftMov;

    private float[] _frontPos;
    private float[] _backPos;
    private float[] _rightPos;
    private float[] _leftPos;

    private void Start()
    {
        _dirFront = tileSize / 2;
        _dirBack = tileSize / 2 * -1;
        _dirRight = tileSize / 2;
        _dirLeft = tileSize / 2 * -1;

        _movePos = tileSize * 3; // 3 * 3 타일 => 타일 세 칸 넓이 이동

        _frontSwap = new int[] { 6, 3, 0, 7, 4, 1, 8, 5, 2 };
        _backSwap = new int[] { 0, 3, 6, 1, 4, 7, 2, 5, 8 };
        _rightSwap = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        _leftSwap = new int[] { 2, 1, 0, 5, 4, 3, 8, 7, 6 };

        _frontMov = new int[] { 0, 1, 2 };
        _backMov = new int[] { 6, 7, 8 };
        _rightMov = new int[] { 2, 5, 8 };
        _leftMov = new int[] { 0, 3, 6 };

        _frontPos = new float[] { 0, 0, _movePos, 0, 0, tileSize };
        _backPos = new float[] { 0, 0, -_movePos, 0, 0, -tileSize };
        _rightPos = new float[] { _movePos, 0, 0, tileSize, 0, 0 };
        _leftPos = new float[] { -_movePos, 0, 0, -tileSize, 0, 0 };

        // Balltan _balltanScript = balltan.GetComponent<Balltan>();
        // Boss2 _boss2Script = boss2.GetComponent<Boss2>();
    }

    void Update()
    {
        // 보스 생존 플래그 설정
        // _isBalltanLive = _balltanScript.isAlive;
        // _isBoss2Live = _boss2Script.isAlive;

        if (GameDataManager.Instance.KillScore >= 300 || Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(MoveFence(bossObj[0]));
            StartCoroutine(MoveUnder(bossObj[2], _targetY));
            _isBalltanLive = true;
            poolManager.SetActive(false);
        }

        if (_isBalltanLive && !_isFence)
        {
            StartCoroutine(MoveFence(bossObj[0]));
            _isFence = true;
        }
        else if (!_isBalltanLive && _isFence)
        {
            StartCoroutine(RemoveObj(bossObj[0]));
            _isFence = false;
        }

        if (_isBoss2Live && !_isCastle)
        {
            StartCoroutine(MoveUnder(bossObj[1], _castleTargetY));
            _isCastle = true;
        }
        else if (!_isBoss2Live && _isCastle)
        {
            StartCoroutine(RemoveObj(bossObj[1]));
            _isCastle = false;
        }

        if (_isPortal)
        {
            StartCoroutine(MoveUnder(bossObj[2], _castleTargetY));
            _isPortal = false;
        }

        // 보스 생존 시 맵 이동 제한
        if (_isBalltanLive || _isBoss2Live)
        {
            return;
        }

        _posX = player.gameObject.transform.position.x;
        _posZ = player.gameObject.transform.position.z;

        // 지정해둔 타일 거리보다 더 이동했으면 타일 이동 후 위치에 맞춰서 거리값 변경

        if (_posZ >= _dirFront)
        {
            MoveMap(_frontSwap, _frontMov, _frontPos);
            _dirFront += tileSize;
            _dirBack += tileSize;
        }

        if (_posZ <= _dirBack)
        {
            MoveMap(_backSwap, _backMov, _backPos);
            _dirFront -= tileSize;
            _dirBack -= tileSize;
        }

        if (_posX >= _dirRight)
        {
            MoveMap(_rightSwap, _rightMov, _rightPos);
            _dirRight += tileSize;
            _dirLeft += tileSize;
        }

        if (_posX <= _dirLeft)
        {
            MoveMap(_leftSwap, _leftMov, _leftPos);
            _dirRight -= tileSize;
            _dirLeft -= tileSize;
        }
    }

    void MoveMap(int[] swapList, int[] objList, float[] posList)
    {
        Swap(mappingObj, swapList);
        for (int i = 0; i < 3; i++)
        {
            mappingObj[objList[i]].transform.position += new Vector3(posList[0], posList[1], posList[2]);
            bossObj[i].transform.position += new Vector3(posList[3], posList[4], posList[5]);
        }
    }

    void Swap(GameObject[] mapTile, int[] sl)
    {
        for (int i = 0; i < 7; i = i + 3)
        {
            GameObject dummy = mapTile[sl[i]];
            mapTile[sl[i]] = mapTile[sl[i + 1]];
            mapTile[sl[i + 1]] = mapTile[sl[i + 2]];
            mapTile[sl[i + 2]] = dummy;
        }
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

    IEnumerator RemoveObj(GameObject obj)
    {
        Vector3 startPosition = obj.transform.position;
        float elapsed = 0f;

        while (elapsed < _reDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _duration;
            Vector3 newPosition = new Vector3(obj.transform.position.x,
                Mathf.LerpUnclamped(startPosition.y, _reTargetY, t), obj.transform.position.z);
            obj.transform.position = newPosition;
            yield return null;
        }

        Vector3 finalPosition = new Vector3(obj.transform.position.x, _reTargetY, obj.transform.position.z);
        obj.transform.position = finalPosition;
    }

    IEnumerator MoveUnder(GameObject under, float underTargetY)
    {
        Vector3 startPosition = under.transform.position;
        float elapsed = 0f;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _duration;
            Vector3 newPosition = new Vector3(under.transform.position.x,
                Mathf.LerpUnclamped(startPosition.y, underTargetY, t), under.transform.position.z);
            under.transform.position = newPosition;
            yield return null;
        }

        Vector3 finalPosition = new Vector3(under.transform.position.x, underTargetY, under.transform.position.z);
        under.transform.position = finalPosition;
    }
}