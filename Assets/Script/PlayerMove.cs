using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h =Input.GetAxis("Horizontal");//수평 입력 받는다
        float v =Input.GetAxis("Vertical");//횡 
        
        
        Vector3 dir = new Vector3(h, 0, v);
        transform.position += dir * moveSpeed * Time.deltaTime;//더함
    }
}
