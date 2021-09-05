using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("PlayerState")]
    public float maxHp;
    public float currentHp;
    public int deffence;
    public int money;
    public bool isDead = false;



    private void Update()
    {
        if (currentHp <= 0)
        {
            isDead = true;
            GameManager.instance.GameOver();
        }
            


        if (!isDead)
        {

        }
        else
            return;


    }




}
