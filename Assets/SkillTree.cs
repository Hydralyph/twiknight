using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    private void Awake() => skillTree = this;

    public int[] SkillLevels;
    public int[] SkillCaps;
    public string[] SkillNames;
    public string[] SkillDescriptions;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;

    public int SkillPoint;

    private void Start()
    {
        SkillPoint = 20; // amount of soul points you have will add this to a feature when enemy dies you gain this
        SkillLevels = new int[6];
        SkillCaps = new[] { 1, 5, 5, 2, 10, 10 };

        SkillNames = new[] {"1", "2", "3", "4", "5", "6", };
        SkillDescriptions = new[]
        {
            "+1 HEART",
            "+5 HEART",
            "+1 DEFENSE",
            "+1 ATTACK",
            "LOCKED",
            "LOCKED",
        };
        // for each skill in skillholder get the componets in skill and add to skill list
        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill); // checks unity for files with skill script and add to list
        foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);

        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;


        SkillList[0].ConnectedSkills = new[] { 1 }; // connecting skills via boxes
        SkillList[1].ConnectedSkills = new[] { 2,3 }; // skill 1 can access boxes 2 and 3
        SkillList[3].ConnectedSkills = new[] { 4, 5 };

        UpdateAllSkillUI();
    }
    public void UpdateAllSkillUI()
    {
        foreach (var skill in SkillList) skill.UpdateUi();
    }
}
