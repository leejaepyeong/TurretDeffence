using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Upgrade", menuName = "Data/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public enum upgradeTypes {DAMAGE, DEFFENCE };
    public upgradeTypes upgradeType;

    public int upgradeCount = 0;
    public int upgradeCost = 100;
    public float value;


    public string upgradeTitle;
    public string upgradeInfoTxt;
    public string towerInfoTxt;


    public void UpgradeOn()
    {
        upgradeCount++;
        upgradeCost += 200;
        value++;
    }
}
