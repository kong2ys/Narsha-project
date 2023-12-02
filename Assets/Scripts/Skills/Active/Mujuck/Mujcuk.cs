using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mujcuk : MonoBehaviour
{
    private bool _isCoolTime = false;
    private float _coolTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(_coolTime);
        _isCoolTime = false;
    }
}
