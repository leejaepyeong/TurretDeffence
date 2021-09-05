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

    public bool isDead;
    public bool isAttack;
    public bool isWalk = true;

    public Slider healthbar;
    private RectTransform barParentTrans; //bar rect transform

    public enum enemyTypes { Small, Large, None };
    public enemyTypes enemyType;

    public Animation anim;
    public AnimationClip attackAnim;
    public AnimationClip walkAnim;
    public AnimationClip deathAnim;
    public AudioSource audio;
    public Rigidbody rigid;
    public NavMeshAgent nav;
    public AudioClip attackSound;
    public AudioClip deadSound;

    public GameObject Target;
    private GameObject playerObj;
    private Player player;
    private Turret turret;

    private void Start()
    {

        Target = GameObject.FindGameObjectWithTag("playerHp");
        nav.destination = Target.transform.position;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();

        nav.speed = moveSpeed;
        anim.Play(walkAnim.name);

        if (healthbar)
            barParentTrans = healthbar.transform.parent.GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        Quaternion rot = Camera.main.transform.rotation;

        if (barParentTrans)
            barParentTrans.rotation = rot;
    }

    void TryAttack()
    {
        if(!isDead && !isAttack)
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        audio.clip = attackSound;
        audio.Play();
        anim.Play(attackAnim.name);
        yield return new WaitForSeconds(0.3f);

        player.currentHp -= (damage - player.deffence);

        yield return new WaitForSeconds(delay);



        isAttack = true;
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

        audio.clip = deadSound;
        audio.Play();
        anim.Play(deathAnim.name);

        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "playerHp")
        {
            isWalk = false;

            nav.isStopped = true;
            TryAttack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "playerHp")
        {
            nav.isStopped = false;
            anim.Play(walkAnim.name);
        }
    }
}
