using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class SkillUI : MonoBehaviour
{
    public Sprite[] skillIcon;
    public Image[] activeSkillUI;

    void Update()
    {
        if (GameDataManager.Instance.GrenadeLevel >= 1)
        {
            activeSkillUI[0].sprite = skillIcon[0];
        }
        
        if (GameDataManager.Instance.TurretLevel >= 1)
        {
            activeSkillUI[1].sprite = skillIcon[1];
        }
    }
}
