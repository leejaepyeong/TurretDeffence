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
    public GameObject Castle;
    public GameObject destroyEffect;


    // Player Death
    public void PlayerDeath()
    {
        isDead = true;
        GameManager.instance.GameOver();


        destroyEffect.SetActive(true);
        Destroy(Castle, 2.5f);

    }


    // Enemy Attack Player
    public void HitPlayer()
    {
        hpBar.fillAmount = currentHp / maxHp;
    }

}
