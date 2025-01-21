using System;
using System.Collections;
using UnityEngine;

namespace Map
{
    public class StructureController : MonoBehaviour
    {
        // 보스 구조물 오브젝트 ([0]: fence / [1]: castle / [2]: portal)
        public GameObject[] bossObj; 
        
        public float duration = 1f;       // 구조물 등장 소요 시간
        public float reDuration = 5f;     // 구조물 사라짐 소요 시간

        private StructureState balttanState;
        private StructureState boss2State;
        private StructureState boss3State;

        public bool isBalltanLive = false; // 발탄 생존 여부 플래그
        public bool isBoss2Live = false;   // boss2 생존 여부 플래그
        
        private bool _isFence = false;  // fence 등장 상태 플래그
        private bool _isCastle = false; // castle 등장 상태 플래그
        private bool _isPortal = false; // portal 등장 상태 플래그
        
        public void Awake()
        {
            balttanState = gameObject.AddComponent<BalttanState>();
            boss2State = gameObject.AddComponent<Boss2State>();
            boss3State = gameObject.AddComponent<Boss3State>();
        }

        public void ChangeState()
        {
            
        }
        
        
    }
}
