using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static SkillTree;
using UnityEngine.UI; //adding this for colors

public class Skill : MonoBehaviour
{
    public int id;

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkills; // used to determin what skills are connected to each other
    public void UpdateUi()
    {
        TitleText.text = $"{SkillTree.skillTree.SkillNames[id]}"; // only shows the number of soul points required
        //TitleText.text = $"{skillTree.SkillLevels[id]}/{skillTree.SkillCaps[id]}\n{SkillTree.skillTree.SkillNames[id]}";
        DescriptionText.text = $"{skillTree.SkillDescriptions[id]}"; // only shows desciption text
        //DescriptionText.text = $"{skillTree.SkillDescriptions[id]}\nCost: {skillTree.SkillPoint}/1 SP";

        GetComponent<Image>().color = skillTree.SkillLevels[id] >= skillTree.SkillCaps[id] ? Color.magenta
            : skillTree.SkillPoint > 1 ? Color.grey : Color.white; // adding colours to let player know what items can be bought and which cannot
   
     // going through connected skills
        foreach ( var connectedSkill in ConnectedSkills)
            {
            skillTree.SkillList[connectedSkill].gameObject.SetActive(skillTree.SkillLevels[id] > 0);
            skillTree.ConnectorList[connectedSkill].SetActive(skillTree.SkillLevels[id] > 0);
        }


    }

    public void Buy() // for buying skills with soul points
    {
        if (skillTree.SkillPoint < 1 || skillTree.SkillLevels[id] >= skillTree.SkillCaps[id]) return;
        skillTree.SkillPoint -= 1;
        skillTree.SkillLevels[id]++;
        skillTree.UpdateAllSkillUI();
    }
}
