using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class SelectSkill : MonoBehaviour
{
    public Image[] selectButton;
    public Image[] passiveSkill;
    private int _pivot = 0;

    public int total = 0;

    public List<int> skillIndex = new List<int>();
    
    public List<Skill> skills = new List<Skill>();
    public List<string> randomSkill = new List<string>();

    public int skillValue;

    void OnEnable()
    {
        Time.timeScale = 0;

        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].skillLevel >= 5)
            {
                Debug.Log(skills[i]);
                skills[i].weight = 0;
            }
            else
            {
                skills[i].weight = 100;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < skills.Count; j++)
            {
                total += skills[j].weight;
            }
            
            int weight = 0;
            int selectNum = Random.Range(0, total);
            
            for (int j = 0; j < skills.Count; j++)
            {
                weight += skills[j].weight;
                if (selectNum < weight)
                {
                    selectButton[i].sprite = skills[j].skillImage;
                    skills[j].weight = 0;
                    skillIndex.Add(j);
                    total = 0;
                    break;
                }
            }
        }
    }

    void HaveSkill(int skillIndex)
    {
        if (skills[skillIndex].skillLevel == 1)
        {
            passiveSkill[_pivot].sprite = skills[skillIndex].skillImage;
            _pivot++;
        }
    }

    public void ButtonLeft()
    {
        skills[skillIndex[0]].skillLevel++;
        Debug.Log(skills[skillIndex[0]].skillName);
        skillValue = skills[skillIndex[0]].value;
        SkillLevelUp(skillValue);
        HaveSkill(skillIndex[0]);
        Finish();
    }

    public void ButtonMiddle()
    {
        skills[skillIndex[1]].skillLevel++;
        Debug.Log(skills[skillIndex[1]].skillName);
        skillValue = skills[skillIndex[1]].value;
        SkillLevelUp(skillValue);
        HaveSkill(skillIndex[1]);
        Finish();
    }

    public void ButtonRight()
    {
        skills[skillIndex[2]].skillLevel++;
        Debug.Log(skills[skillIndex[2]].skillName);
        skillValue = skills[skillIndex[2]].value;
        SkillLevelUp(skillValue);
        HaveSkill(skillIndex[2]);
        Finish();
    }

    void SkillLevelUp(int value)
    {
        switch (value)
        {
            case 0: // 기본 공격
            {
                if (GameDataManager.Instance.FireLevel < 5)
                {
                    skills[0].skillObject.SetActive(true);
                    GameDataManager.Instance.FireLevel++;
                    Debug.Log("발사렙"+GameDataManager.Instance.FireLevel);
                }
                break;
            }
            case 1: // 파이어볼
            {
                if (GameDataManager.Instance.FireBallLevel < 5)
                {
                    skills[1].skillObject.SetActive(true);
                    GameDataManager.Instance.FireBallLevel++;
                    Debug.Log("불알(파이어볼)렙"+GameDataManager.Instance.FireBallLevel);
                }
                break;
            }
            case 2: // 드론
            {
                if (GameDataManager.Instance.DroneLevel < 5)
                {
                    skills[2].skillObject.SetActive(true);
                    GameDataManager.Instance.DroneLevel++;
                    Debug.Log("드론렙"+GameDataManager.Instance.DroneLevel);
                }
                break;
            }
            case 3: // 화살
            {
                if (GameDataManager.Instance.ArrowLevel < 5)
                {
                    skills[3].skillObject.SetActive(true);
                    GameDataManager.Instance.ArrowLevel++;
                    Debug.Log("활렙"+GameDataManager.Instance.ArrowLevel);
                }

                break;
            }
            case 4: // 아이스에이지
            {
                if (GameDataManager.Instance.IceLevel < 5)//게임데이터매니저에서 레벨추가
                {
                    skills[4].skillObject.SetActive(true);
                    GameDataManager.Instance.IceLevel++;
                    Debug.Log("아에~!"+GameDataManager.Instance.IceLevel);
                }
                
                break;
            }
            case 5: // 아이스볼
            {
                if (GameDataManager.Instance.IceBallLevel < 5)//게임데이터매니저에서 레벨추가
                {
                    skills[5].skillObject.SetActive(true);
                    GameDataManager.Instance.IceBallLevel++;
                    Debug.Log("아이스볼"+GameDataManager.Instance.IceBallLevel);
                }
                
                break;
            }
            case 6: // 포이즌볼
            {
                if (GameDataManager.Instance.PoisonBallLevel < 5)//게임데이터매니저에서 레벨추가
                {
                    skills[6].skillObject.SetActive(true);
                    GameDataManager.Instance.PoisonBallLevel++;
                    Debug.Log("포이즌볼"+GameDataManager.Instance.PoisonBallLevel);
                }
                
                break;
            }
            case 7: // 검방패
            {
                if (GameDataManager.Instance.SwordShieldLevel < 5)//게임데이터매니저에서 레벨추가
                {
                    skills[7].skillObject.SetActive(true);
                    GameDataManager.Instance.SwordShieldLevel++;
                    Debug.Log("검방패"+GameDataManager.Instance.SwordShieldLevel);
                }
                
                break;
            }
            case 8: // 공격력 증가
            {
                if (skills[8].skillLevel < 5)
                {
                    skills[8].skillObject.SetActive(true);
                    Debug.Log("공격력 증가 "+GameDataManager.Instance.PlusStr);
                }
                
                break;
            }
            case 9: // 체력 증가
            {
                if (skills[9].skillLevel < 5)
                {
                    skills[9].skillObject.SetActive(true);
                    Debug.Log("체력증가" + GameDataManager.Instance.PlusHp);
                }
                break;
            }
            case 10: // 이속 증가
            {
                if (skills[10].skillLevel < 5)
                {
                    skills[10].skillObject.SetActive(true);
                    Debug.Log("dㅣ속증가" + GameDataManager.Instance.PlusDex);
                }
                break;
            }
            case 11: // 메테오
            {
                if (skills[11].skillLevel < 5)
                {
                    skills[11].skillObject.SetActive(true);
                }

                break;
            }
            case 12: // 바운스볼
            {
                if (skills[12].skillLevel < 5)
                {
                    GameDataManager.Instance.BounceBallLevel++;
                    skills[12].skillObject.SetActive(true);
                    Debug.Log("바운스볼렙" + GameDataManager.Instance.BounceBallLevel);
                }

                break;
            }
        }
        
    }

    public void Finish()
    {
        total = 0;
        skillIndex.Clear();
        randomSkill.Clear();
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}