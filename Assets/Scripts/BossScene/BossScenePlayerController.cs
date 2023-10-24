using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScenePlayerController : MonoBehaviour
{
    public float rotSpeed = 200.0f;
    private float _mx;
    
    public float moveSpeed = 7.0f;
    private float _gravity = -20.0f;
    private float _yVelocity = 0;
    private bool _isJumping = false;
    public float jumpPower = 5.0f;

    private Vector3 _dir;
    
    CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        _dir = new Vector3(h, 0, v);
        _dir = _dir.normalized;
        _dir = Camera.main.transform.TransformDirection(_dir);

        transform.position += _dir * (moveSpeed * Time.deltaTime);

        _yVelocity += _gravity * Time.deltaTime;
        _dir.y = _yVelocity;
        _characterController.Move(_dir * (moveSpeed * Time.deltaTime));

        Jump();
        PlayerRotate();
    }

    void Jump()
    {
        if (_characterController.collisionFlags == CollisionFlags.Below) // https://sshoreng.tistory.com/122
        {
            if (_isJumping)  
            {
                _isJumping = false;
                _yVelocity = 0;
            }
        }
        
        if (Input.GetAxis("Jump") != 0 && !_isJumping)
        {
            _yVelocity = jumpPower;
            _isJumping = true;
        }
    }

    void PlayerRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");

        _mx += mouseX * rotSpeed * Time.deltaTime;
        
        transform.eulerAngles = new Vector3(0, _mx, 0);
    }
}
