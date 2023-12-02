using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mujcuk : MonoBehaviour
{
    private bool _isCoolTime = false;
    private float _coolTime = 30.0f;

    public bool _isMujuck = false;

    private float duration = 3.0f;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !_isCoolTime)
        {
            Mujuck();
        }
    }

    public void Mujuck()
    {
        
            _isCoolTime = true;
            Debug.Log("gg");
            _isMujuck = true;
            StartCoroutine(CoolTime());
            StartCoroutine(MujuckDuration());

    }
    IEnumerator CoolTime()
    {
        
        yield return new WaitForSeconds(_coolTime);
        _isCoolTime = false;
    }

    IEnumerator MujuckDuration()
    {
        yield return new WaitForSeconds(duration);
        _isMujuck = false;
    }
}
