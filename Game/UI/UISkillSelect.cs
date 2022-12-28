using System.Collections.Generic;
using UnityEngine;

public class UISkillSelect : GameEventListener
{
    [Header("Skill Upgrade List", order = 0)]
    [Header("Skill Instantiate Effect Must Be In Index 0", order = 1)]
    [SerializeField] List<AutoGunUpgrade> autoGunUpgradeList;
    [SerializeField] List<CornThrowerUpgrade> cornThrowerUpgradeList;
    [SerializeField] List<SpinningShovelUpgrade> spinningShovelUpgradeList;
    [SerializeField] SkillSelectButton[] selectButtons;

    bool isFirstSelect;

    new void OnEnable()
    {
        base.OnEnable();
        SetSkillSelectUI();
        Time.timeScale = 0f;
    }

    void Start()
    {
        Response.AddListener(DeActivate);
    }

    public void DeActivate()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void SetSkillSelectUI()
    {
        /// 추첨할 리스트 객체 선언
        List<SkillUpgrade> upgradeList = new List<SkillUpgrade>();

        if (!isFirstSelect)
        {
            upgradeList.Add(autoGunUpgradeList[0]);
            upgradeList.Add(cornThrowerUpgradeList[0]);
            upgradeList.Add(spinningShovelUpgradeList[0]);
            isFirstSelect = true;

            for (int i = 0; i < selectButtons.Length; i++) 
            {
                selectButtons[i].SetSkillButtonSlot(upgradeList[i]);
            }
            return;
        }
        
        if (!GameObject.FindGameObjectWithTag("AutoGun"))
        {
            upgradeList.Add(autoGunUpgradeList[0]);
        }
        else
        {
            for (int i = 1; i < autoGunUpgradeList.Count; i++)
            {
                upgradeList.Add(autoGunUpgradeList[i]);
            }
        }

        if (!GameObject.FindGameObjectWithTag("CornThrower"))
        {
            upgradeList.Add(cornThrowerUpgradeList[0]);
        }
        else
        {
            for (int i = 1; i < cornThrowerUpgradeList.Count; i++)
            {
                upgradeList.Add(cornThrowerUpgradeList[i]);
            }
        }

        if (!GameObject.FindGameObjectWithTag("SpinningShovel"))
        {
            upgradeList.Add(spinningShovelUpgradeList[0]);
        }
        else
        {
            for (int i = 1; i < spinningShovelUpgradeList.Count; i++)
            {
                upgradeList.Add(spinningShovelUpgradeList[i]);
            }
        }

        for (int i = 0; i < selectButtons.Length; i++) 
        {
            int random = Random.Range(0, upgradeList.Count);
            selectButtons[i].SetSkillButtonSlot(upgradeList[random]);
            upgradeList.RemoveAt(random);
        }
    }
}