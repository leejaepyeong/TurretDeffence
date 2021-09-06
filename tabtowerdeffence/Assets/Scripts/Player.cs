using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [Header("PlayerState")]
    public float maxHp;
    public float currentHp;
    public int deffence;
    public bool isDead = false;

    public Image hpBar;



    public void PlayerDeath()
    {
        isDead = true;
        GameManager.instance.GameOver();
    }

    public void HitPlayer()
    {
        hpBar.fillAmount = currentHp / maxHp;
    }

}
