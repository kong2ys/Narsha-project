using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransForm;
    Vector3 Offset;
    void Awake()
    {
        playerTransForm = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransForm.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerTransForm.position + Offset;
    }
}