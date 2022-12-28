using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectButton : MonoBehaviour
{
    SkillUpgrade skillUpgrade;
    [SerializeField] GameEvent OnClickSkillButton;
    [SerializeField] Image skillIcon;
    [SerializeField] TextMeshProUGUI skillDescription;

    public void SetSkillButtonSlot(SkillUpgrade skill)
    {
        skillUpgrade = skill;
        skillIcon.sprite = skill.image;
        skillDescription.text = skill.description;
    }

    public void OnClickButton()
    {
        skillUpgrade.OnSelect();
        OnClickSkillButton.Raise();
    }
}