using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;

    public Transform target;
    public GameObject effectHitPrefab;
    public string weaponeType;

    public UpgradeData upgradeData;


    private void Start()
    {
        damage = upgradeData.value;
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
         if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - gameObject.transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void DamageUp()
    {
        damage = upgradeData.value;
    }

    void HitTarget()
    {

        GameObject effectHit = Instantiate(effectHitPrefab, transform.position, transform.rotation);
        Destroy(effectHit,0.7f);

        target.gameObject.GetComponent<Enemy>().Hit(damage, weaponeType);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target.gameObject)
        {
            HitTarget();
        }
    }
}
