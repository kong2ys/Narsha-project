using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkill : MonoBehaviour
{
    public List<Skill> deck = new List<Skill>();  // 카드 덱
    
    void OnEnable()
    {
        Debug.Log(deck[0].skillName);
        Time.timeScale = 0;
    }

    void Select()
    {
        
    }
}
