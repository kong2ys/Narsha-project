using System.Collections;
using UnityEngine;

namespace Map
{
    public interface IMoveObjects
    {
        void CheckFlags();
        
        IEnumerator MoveObject(GameObject obj, float duration, float targetY);
    }
}