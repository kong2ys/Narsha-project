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
