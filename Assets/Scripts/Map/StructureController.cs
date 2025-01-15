using System;
using System.Collections;
using UnityEngine;

namespace Map
{
    public class StructureController : MonoBehaviour
    {
        private float _duration = 1f;       // 구조물 등장 소요 시간
        private float _reDuration = 5f;     // 구조물 사라짐 소요 시간

        private IStructureState balttanState;
        private IStructureState boss2State;
        private IStructureState boss3State;

        public void Awake()
        {
            balttanState = gameObject.AddComponent<BalttanState>();
            boss2State = gameObject.AddComponent<Boss2State>();
            boss3State = gameObject.AddComponent<Boss3State>();
        }

        public void ChangeState()
        {
            
        }
        
        public IEnumerator MoveStructure(GameObject obj, float duration, float targetY)
        {
            Vector3 startPosition = obj.transform.position;
            float elapsed = 0f;

            while (elapsed < _reDuration) 
            {
                elapsed += Time.deltaTime;
                float t = elapsed / _duration;
                Vector3 newPosition = new Vector3(obj.transform.position.x,
                    Mathf.LerpUnclamped(startPosition.y, targetY, t), obj.transform.position.z);
                obj.transform.position = newPosition;
                yield return null;
            }

            Vector3 finalPosition = new Vector3(obj.transform.position.x, targetY, obj.transform.position.z);
            obj.transform.position = finalPosition;
        }
    }
}
