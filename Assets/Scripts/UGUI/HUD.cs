using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    Text _mytext;
    Slider _mySlider;

    void Awake()
    {
        _mytext = GetComponent<Text>();
        _mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
            {
                float currentExp = GameDataManager.Instance.CurrentExp;
                float maxExp = GameDataManager.Instance.MaxExp;
                _mySlider.value = currentExp / maxExp;
                break;
            }
            case InfoType.Level:
            {
                
                break;
            }
            case InfoType.Kill:
            {
                
                break;
            }
            case InfoType.Time:
            {
                float currentTime = GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(currentTime / 60);
                int sec = Mathf.FloorToInt(currentTime % 60);
                _mytext.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            }
            case InfoType.Health:
            {
                float currentHp = GameDataManager.Instance.PlayerHp;
                float maxHp = GameDataManager.Instance.PlayerMaxHp + GameDataManager.Instance.PlusHp;
                _mySlider.value = currentHp / maxHp;
                break;
            }
            
        }
    }
}
