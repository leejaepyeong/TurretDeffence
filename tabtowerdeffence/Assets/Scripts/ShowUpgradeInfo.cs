using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowUpgradeInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TowerUpgrade towerUpgrade;

    public void OnPointerEnter(PointerEventData eventData)
    {
        towerUpgrade.upgradeInfoPanel.SetActive(true);

        towerUpgrade.ShowUpgradeInfo();


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        towerUpgrade.upgradeInfoPanel.SetActive(false);
    }
}
