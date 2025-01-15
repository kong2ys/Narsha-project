using System.Collections;
using UnityEngine;

namespace Map
{
    public interface IMoveStructures
    {
        void CheckFlags();
        
        IEnumerator MoveStructure(GameObject obj, float duration, float targetY);
    }
}