using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScenePlayerController : MonoBehaviour
{
    public float rotSpeed = 200.0f;
    private float _mx;
    
    public float moveSpeed;
    private float _gravity = -20.0f;
    private float _yVelocity = 0;
    private bool _isJumping = false;
    public float jumpPower = 5.0f;
    
    public float stopMove = 0;
    public float currentTime;
    
    public GameObject[] attack;
    private int _attackMotion = 0;

    public float attackPower = 5.0f;

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
        if (stopMove >= 0)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            _dir = new Vector3(h, 0, v);
            _dir = _dir.normalized;
            _dir = Camera.main.transform.TransformDirection(_dir);

            if (stopMove > 0)
            {
                stopMove -= Time.deltaTime;
                moveSpeed = 0;
            }
            else
                moveSpeed = 20f;

            transform.position += _dir * (moveSpeed * Time.deltaTime);

            _yVelocity += _gravity * Time.deltaTime;
            _dir.y = _yVelocity;
            _characterController.Move(_dir * (moveSpeed * Time.deltaTime));
        }
        else if(stopMove < 0)
        {
            stopMove = 0;
        }

        Jump();
        PlayerRotate();
        Hit();
    }
    
    public void DamageAction(float damage) // 데미지 입기
    {
        GameDataManager.Instance.PlayerHp -= damage;
        Debug.Log("남은 HP : " + GameDataManager.Instance.PlayerHp);
    }
    
    void Hit()
    {
        if (currentTime <= 0)
        {
            currentTime = 0;
            for (int i = 0; i < 3; i++)
            {
                if (attack[i].activeSelf)
                    attack[i].SetActive(false);
            }
        }

        if (currentTime == 0 && stopMove == 0 && Input.GetMouseButton(0))
        {
            _attackMotion++;
            stopMove = 1f;
            currentTime = 0.5f;
            if (_attackMotion > 2)
                _attackMotion = 0;
        }

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            attack[_attackMotion].SetActive(true);
        }
        
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
