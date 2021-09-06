using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public float range;
    public float fireRate;
    private float fireCountDown = 0f;

    public enum turretTypes {Melee, Boom, Pierce };
    public turretTypes turretType;

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed;

    public GameObject bulletPrefab;
    public Transform firePoint;


    // Find Targe
    void UpdateTarget(GameObject _other)
    {
        target = _other.transform;

        /*
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearesetEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearesetEnemy = enemy;
            }
        }

        if(nearesetEnemy != null && shortestDistance <= range)
        {
            target = nearesetEnemy.transform;
        }
        else
        {
            target = null;
        }
        */
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);

        if(fireCountDown <= 0f && !target.gameObject.GetComponent<Enemy>().isDead)
        {
            Shoot();
            fireCountDown = fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }


    void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.weaponeType = turretType.ToString();

        if (bullet != null)
            bullet.Seek(target);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        {

            if (target == null || target.gameObject.GetComponent<Enemy>().isDead)
                UpdateTarget(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == enemyTag)
        {
            if(target == null || target.gameObject.GetComponent<Enemy>().isDead)
            UpdateTarget(other.gameObject);
        }
    }

}
