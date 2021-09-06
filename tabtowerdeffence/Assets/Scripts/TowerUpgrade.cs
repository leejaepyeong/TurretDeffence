using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerUpgrade : MonoBehaviour, IPointerClickHandler
{
    public GameObject upgradeInfoPanel;
    public Text upgradeTitle;
    public Text upgradeInfoTxt;
    public Text TowerValue;
    public Text upgradeCostTxt;
    public Text upgradeCountTxt;


    public UpgradeData upgradeData;
    public Bullet bullet;
    public Player player;


    private void Start()
    {
        upgradeTitle.text = upgradeData.upgradeTitle;
        upgradeInfoTxt.text = upgradeData.upgradeInfoTxt;
        upgradeCostTxt.text = upgradeData.upgradeCost.ToString();

        TowerValue.text = upgradeData.towerInfoTxt + upgradeData.value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.instance.gold >= upgradeData.upgradeCost)
        {
            GameManager.instance.UpgradeCost(upgradeData.upgradeCost);
            upgradeData.UpgradeOn();

            switch(upgradeData.upgradeType)
            {
                case UpgradeData.upgradeTypes.DAMAGE:
                    bullet.DamageUp();
                    break;
                case UpgradeData.upgradeTypes.DEFFENCE:
                    player.deffence = (int)upgradeData.value;
                    break;
            }

            ShowUpgradeInfo();
        }
            
    }

    public void ShowUpgradeInfo()
    {
        upgradeTitle.text = upgradeData.upgradeTitle;
        upgradeInfoTxt.text = upgradeData.upgradeInfoTxt;
        upgradeCostTxt.text = upgradeData.upgradeCost.ToString();
        upgradeCountTxt.text = upgradeData.upgradeCount.ToString();

        TowerValue.text = upgradeData.towerInfoTxt + upgradeData.value;
    }

}
