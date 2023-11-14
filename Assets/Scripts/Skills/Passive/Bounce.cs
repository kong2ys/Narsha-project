using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    float _moveSpeed = 30.0f;
    float _moveXRate;
    float _moveYRate;
    Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        _moveXRate=Random.Range(-1.0f,1.0f);
        _moveYRate=Random.Range(-1.0f,1.0f);
        while(Mathf.Abs(_moveXRate)<0.3f)
        {
            _moveXRate=Random.Range(-1.0f,1.0f);
        }
        while(Mathf.Abs(_moveYRate)<0.3f)
        {
            _moveYRate=Random.Range(-1.0f,1.0f);
        }
    }
    
    void Update()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * _moveSpeed * _moveXRate),Space.World);
        transform.Translate(Vector3.forward * (Time.deltaTime * _moveSpeed * _moveYRate),Space.World);
        
        Vector3 position = _camera.WorldToViewportPoint(transform.position);
        
        if (position.x < 0f)
        {
            position.x=0f;
            _moveXRate=Random.Range(0.3f,1.0f);
        }
        if (position.y<0f)
        {
            position.y=0f;
            _moveYRate=Random.Range(0.3f,1.0f);
        }
        if(position.x>1f)
        {
            position.x=1f;
            _moveXRate=Random.Range(-1.0f,-0.3f);
        }
        if(position.y>1f)
        {
            position.y=1f;
            _moveYRate=Random.Range(-1.0f,-0.3f);
        }
        transform.position=_camera.ViewportToWorldPoint(position);
    }
}
