using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MoveObjects : MonoBehaviour, IMoveObjects
    {
        public GameObject[] bossObj;    // 보스 구조물 오브젝트 ([0]: fence / [1]: castle / [2]: portal)
    
        private float _duration = 1f;       // 구조물 등장 소요 시간
        private float _reDuration = 5f;     // 구조물 사라짐 소요 시간
        private float _targetY = 0;         // 구조물 올라오는 위치
        private float _castleTargetY = 20;  // castle 구조물 올라오는 위치
        private float _reTargetY = -100;    // 구조물 내려가는 위치\
    
        private bool _isBalltanLive = false; // 발탄 생존 여부 플래그
        private bool _isBoss2Live = false;   // boss2 생존 여부 플래그

        private Balltan _balltanScript;    // 발탄 스크립트 참조
        // private Boss2 _boss2Script;     // boss2 스크립트 참조
    
        private bool _isFence = false;  // fence 등장 상태 플래그
        private bool _isCastle = false; // castle 등장 상태 플래그
        private bool _isPortal = false; // portal 등장 상태 플래그

        private void Start()
        {
            // Balltan _balltanScript = balltan.GetComponent<Balltan>();
            // Boss2 _boss2Script = boss2.GetComponent<Boss2>();
        }

        public void CheckFlags()
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
        }

        public IEnumerator MoveObject(GameObject obj, float duration, float targetY)
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
    }
}

