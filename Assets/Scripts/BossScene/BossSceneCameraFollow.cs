using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    
    public float rotSpeed = 200.0f;

    private float _mx;
    private float _my;

    void Update()
    {
        transform.position = (target.position + offset);
        
        CamRotate();
    }

    void CamRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _mx += mouseX * rotSpeed * Time.deltaTime;
        _my += mouseY * rotSpeed * Time.deltaTime;

        _my = Mathf.Clamp(_my, -90f, 90f);

        transform.eulerAngles = new Vector3(-_my, _mx, 0);
    }
}
