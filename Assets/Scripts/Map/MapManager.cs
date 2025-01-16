using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance;
        
        public GameObject[] mappingObj; // 9개 타일 오브젝트
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

        public MoveStructures moveStructures;
        public StructureController structureController;

        public bool isBalltanLive = false; // 발탄 생존 여부 플래그
        public bool isBoss2Live = false;   // boss2 생존 여부 플래그
        
        
        private GameDataManager _gmScript; // 게임 데이터 매니저 참조


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

        private void Awake()
        {
            if (Instance = null)
            {
                Instance = this;
            }

            structureController = gameObject.GetComponent<StructureController>();
            moveStructures = gameObject.AddComponent<MoveStructures>();
            
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

            
        }

        void Update()
        {
            

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
}
