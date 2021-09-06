using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashEffect : MonoBehaviour
{
    float damage;
    public Bullet bullet;

    private void Start()
    {
        damage = (bullet.damage) / 2 ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.Hit(damage, "Boom");
        }
    }
}
