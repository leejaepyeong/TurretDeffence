using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float MaxHp;
    public float Hp;
    public float damage;
    public float deffence;
    public float moveSpeed;
    public float range;
    public float delay;

    public bool isBoss;
    public bool isDead;
    public bool isAttack;
    public bool isWalk = true;

    public Slider healthbar;
    private RectTransform barParentTrans; //bar rect transform

    public GameObject attackEffect;

    public enum enemyTypes { Small, Large, None };
    public enemyTypes enemyType;

    public Animator anim;
    public AudioSource audio;
    public Rigidbody rigid;
    public NavMeshAgent nav;
    public AudioClip attackSound;
    public AudioClip deadSound;

    public GameObject Target;
    private GameObject playerObj;
    public Player player;

    private void Start()
    {

        Target = GameObject.FindGameObjectWithTag("playerHp");
        nav.destination = Target.transform.position;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();

        nav.speed = moveSpeed;
        isWalk = true;
        anim.SetBool("Walk", isWalk);

        if (healthbar)
            barParentTrans = healthbar.transform.parent.GetComponent<RectTransform>();


        LevelUp();
    }

    void LateUpdate()
    {
        Quaternion rot = Camera.main.transform.rotation;

        if (barParentTrans)
            barParentTrans.rotation = rot;
    }

    public void LevelUp()
    {
        if(!isBoss)
        {
            MaxHp += 8 * GameManager.instance.level;
            Hp += 8 * GameManager.instance.level;
            damage += 2 * GameManager.instance.level;
            deffence += 1 * GameManager.instance.level;

            return;
        }

        if(isBoss &&  GameManager.instance.level != 0 && GameManager.instance.level % 4 == 0)
        {
            MaxHp += 80 * ((GameManager.instance.level) / 4);
            Hp += 80 * ((GameManager.instance.level) / 4);
            damage += 6 * ((GameManager.instance.level) / 4);
            deffence += 3 * ((GameManager.instance.level) / 4);
        }

        
        
    }

    void TryAttack()
    {
        if(!isDead && !isAttack)
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        isAttack = true;

        audio.clip = attackSound;
        audio.Play();
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.25f);
        attackEffect.SetActive(true);


        float deal = damage - player.deffence;
        if (deal <= 0) deal = 1;
        player.currentHp -= deal;

        player.HitPlayer();

        if (player.currentHp <= 0 && !player.isDead)
        {
            player.PlayerDeath();
        }

        yield return new WaitForSeconds(0.25f);

        attackEffect.SetActive(false);

        yield return new WaitForSeconds(delay);



        isAttack = false;
    }

    public void Hit(float _damage, string _weaponType)
    {
        // type damage
        switch(enemyType)
        {
            case enemyTypes.None:
                switch(_weaponType)
                {
                    case "Melee":
                        _damage *= 0.8f;
                        break;
                    case "Pierce":
                        _damage *= 1.2f;
                        break;
                    case "Boom":
                        break;

                }
                break;
            case enemyTypes.Small:
                switch (_weaponType)
                {
                    case "Melee":
                        _damage *= 1.2f;
                        break;
                    case "Pierce":
                        break;
                    case "Boom":
                        _damage *= 0.8f;
                        break;

                }
                break;
            case enemyTypes.Large:
                switch (_weaponType)
                {
                    case "Melee":
                        
                        break;
                    case "Pierce":
                        _damage *= 0.8f;
                        break;
                    case "Boom":
                        _damage *= 1.2f;
                        break;

                }
                break;
        }

        // limit damage 1
        float deal = _damage - deffence;
        if (deal <= 0) deal = 1;
        Hp -= deal;

        healthbar.value = Hp / MaxHp;

        if (!isDead && Hp <= 0)
        {
            StartCoroutine(Dead());
        }
    }


    IEnumerator Dead()
    {
        isDead = true;
        nav.isStopped = true;

        GameManager.instance.GetGold();

        audio.clip = deadSound;
        audio.Play();
        anim.SetTrigger("Death");

        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "playerHp")
        {
            isWalk = false;
            anim.SetBool("Walk", isWalk);
            nav.isStopped = true;
            TryAttack();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "playerHp")
        {
            TryAttack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "playerHp")
        {
            nav.isStopped = false;
            isWalk = true;
            anim.SetBool("Walk", isWalk);
        }
    }
}
