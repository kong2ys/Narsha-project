using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindWithTag("Player");
    }

    private void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _target.transform.position;
        transform.Rotate(new Vector3(0,45,0)*Time.deltaTime);
    }

}
