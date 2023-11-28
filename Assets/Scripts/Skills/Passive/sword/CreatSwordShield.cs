using System.Collections;
using UnityEngine;

public class CreatSwordShield : MonoBehaviour
{
    public GameObject _shieldPrefeb;
    private GameObject _shield;
    public float _duration = 5f;
    
    private float[] coolTime = {0f,20f, 18f, 16f, 14f, 12f};
    void Awake()
    {
        _shield = Instantiate(_shieldPrefeb);
        _shield.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(CoolTime());
    }

    void MakeShield()
    {
        _shield.SetActive(true);
        StartCoroutine(DeactivateShield());
    }

    IEnumerator DeactivateShield()
    {
        yield return new WaitForSeconds(_duration);
        _shield.SetActive(false);
    }

    IEnumerator CoolTime()
    {
        while (true)
        {
            MakeShield();
            yield return new WaitForSeconds(coolTime[GameDataManager.Instance.SwordShieldLevel]+_duration);
        }
    }
}