using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct SkillToCost
{
    public SkillType type;
    public int cost;
}
[System.Serializable]
public struct SkillToButton
{
    public SkillType type;
    public Button button;
}

[System.Serializable]
public class SkillTreeHandler : MonoBehaviour
{
    public Player player;
    public Text SP_Display;
    [SerializeField]
    public List<SkillToCost> skillCost;
    [SerializeField]
    public List<SkillToButton> skillButtons;

    private void OnEnable()
    {
        Init();
    }
    public void Init()
    {
        SP_Display.text = player.sp.ToString();

        foreach (var item in skillButtons)
        {
            if (player.sp < skillCost.Find(x=> x.type==item.type).cost)
            {
                item.button.enabled = false;
                item.button.GetComponent<Image>().color = Color.red;
            }
            else
            {
                item.button.enabled = true;
                item.button.GetComponent<Image>().color = Color.yellow;
            }

            if (player.unlockedSkills.Contains(item.type))
            {
                item.button.enabled = false;
                item.button.GetComponent<Image>().color = Color.green;
            }
        }
    }
    public void ClickBuySkillButton(SkillType skillType)
    {
        if (player.sp >= skillCost.Find(x => x.type == skillType).cost && !player.unlockedSkills.Contains(skillType))
        {
            player.sp -= skillCost.Find(x => x.type == skillType).cost;
            player.unlockedSkills.Add(skillType);
        }

        Init();
    }

    public void Invalid()
    {
        Debug.Log("InvalidButton :D");

        Init();
    }

    public void ClickRootBullet() => ClickBuySkillButton(SkillType.RootBullet);
    public void ClickRootSpike() => ClickBuySkillButton(SkillType.RootSpike);
    public void ClickRootBurst() => Invalid();
}
